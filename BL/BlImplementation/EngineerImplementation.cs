using BlApi;
using BO;
using DalApi;

//using DalApi;
using DO;
using System.Data.Common;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace BlImplementation;

internal class EngineerImplementation : BlApi.IEngineer
{
    private DalApi.IDal _dal = Factory.Get;

    /********* CRUD functions **********/

    public int Create(BO.Engineer boEngineer)
    {
        if (boEngineer.Id < 0 
            || boEngineer.Name == "" 
            || boEngineer.Name is null
            || boEngineer.Cost < 0
            || !IsValidEmail(boEngineer.Email!))
            throw new BO.BlInvalidvalueException("one of the engineer's values is invalid");
        DO.Engineer doEngineer = new DO.Engineer
            (Id: boEngineer.Id,
            Name: boEngineer.Name,
            Email: boEngineer.Email,
            Level: (DO.EngineerExperience)boEngineer.Level,
            Cost: boEngineer.Cost);
        try
        {
            int engId = _dal.Engineer.Create(doEngineer);
            return engId;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        if ((from t in _dal.Task.ReadAll()//if there is a current task for this engineer
             where IsCurrentTask(id, t.Id)||
             t.EngineerId==id&&t.DateOfFinishing!=null
             select true).FirstOrDefault()!)
            throw new BO.BlDeletionImpossible($"Engineer with ID={id} can not be deleted");
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={id} does Not exist", ex);
        }
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        var task = (from t in _dal.Task.ReadAll()
                        where IsCurrentTask(id, t.Id)
                        select t).FirstOrDefault();
        return new BO.Engineer()
        {
            Id= id,
            Name= doEngineer.Name,
            Email= doEngineer.Email,
            Level= (BO.EngineerExperience)doEngineer.Level,
            Cost= doEngineer.Cost,
            Task = task is null ? null: toTaskInEngineer(task)
        };
    }

    public IEnumerable<BO.EngineerInTask> ReadAll(Func<DO.Engineer, bool>? filter = null)
    {
        return from DO.Engineer doEngineer in _dal.Engineer.ReadAll(filter)
               select ToEngineerInTask(doEngineer);
    }
   
    public void Update(BO.Engineer boEngineer)
    {
        if (boEngineer.Id < 0 || boEngineer.Name == "" || boEngineer.Cost < 0|| !IsValidEmail(boEngineer.Email!))
            throw new BO.BlInvalidvalueException("one of the engineer's values is invalid");
        DO.Engineer? new_doEngineer = new DO.Engineer
             (Id: boEngineer.Id,
             Name: boEngineer.Name,
             Email: boEngineer.Email,
             Level: (DO.EngineerExperience)boEngineer.Level,
             Cost: boEngineer.Cost);
        if (_dal.Task.ReadAll(tsk => tsk.EngineerId == boEngineer.Id).FirstOrDefault() != null)
                throw new BlCantSetValue($"Engineer with id: {boEngineer.Id} already has a task, can't update");//TODO: check if the task is a current task
        
        DO.Task task = (from DO.Task doTask in _dal.Task.ReadAll()
                        let IsGoodForEng = CheckTaskForEngineer(boEngineer.Id, doTask.Id)
                        where IsGoodForEng != null
                        select doTask).FirstOrDefault()!;//returns a task that matches to the engineer
        if (task != null)
            if (task.EngineerId==0)//cheke that the task does'nt belong to another engineer
                _dal.Task.Update(task with { EngineerId = boEngineer.Id });
        try
        {
            _dal.Engineer.Update(new_doEngineer);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={boEngineer.Id} does Not exist", ex);
        }

    }


    /************* Tools  *************/

    public TaskInEngineer toTaskInEngineer(DO.Task task) => 
                   new BO.TaskInEngineer() { Id = task.Id, NickName = task.NickName };
    public bool IsCurrentTask(int engId, int taskId)
    {
        DO.Task task = _dal.Task.Read(taskId)!;
        if (task.EngineerId == engId
            && task.DateOfstratJob != null
            && task.DateOfFinishing == null)
            return true;
        return false;
    }
    public IEnumerable<BO.TaskInEngineer> GetDetailedTaskForEngineer(int engineerId)
    {
        return from DO.Task doTask in _dal.Task.ReadAll()
               let IsGoodForEng = CheckTaskForEngineer(engineerId, doTask.Id)
               where IsGoodForEng != null
               select IsGoodForEng;
    }
    public bool IsValidEmail(string email)
    {
        if (email==null)
            return false;
        // Regular expression pattern for validating email addresses
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }
    public BO.TaskInEngineer? CheckTaskForEngineer(int engineerId, int taskId)
    {
        DO.Task doTask = _dal.Task.Read(taskId)!;
        DO.Engineer doEngineer = _dal.Engineer.Read(engineerId) ??
                                            throw new BO.BlDoesNotExistException($"Engineer with ID={engineerId} does Not exist"); 

        if (doTask.EngineerId is null//the task does not belong to another engineer
            //&& doTask.DateOfFinishing < DateTime.Now
            && doTask.LevelOfDifficulty <= doEngineer.Level
        &&((from dep in _dal.Dependency.ReadAll()
            let depOnTaskId = dep.DependentOnTaskId
            where dep.DependentTaskId == taskId
            && _dal.Task.Read(depOnTaskId)!.DateOfFinishing ==null
            select dep).FirstOrDefault() == null))
            return new BO.TaskInEngineer() { Id = taskId, NickName = doTask.NickName };
        return null;
    }
     public EngineerInTask ToEngineerInTask(DO.Engineer  doEngineer)
    {
        return new BO.EngineerInTask()
        {
            EngineerId = doEngineer.Id,
            Name = doEngineer.Name,
            Level = (BO.EngineerExperience)doEngineer.Level,
            TaskId = (from task in _dal.Task.ReadAll()
                      where IsCurrentTask(doEngineer.Id, task.Id)
                      select task.Id).FirstOrDefault()
        };
    }
}
