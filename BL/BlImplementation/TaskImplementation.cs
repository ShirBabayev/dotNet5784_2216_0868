using BlApi;
using BO;
using DalApi;
using DO;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlImplementation;

internal class TaskImplementation : BlApi.ITask
{
    private DalApi.IDal _dal = Factory.Get;

    public void StartProject(DateTime InitDate) => _dal.InitDate = InitDate;

    public void EndProject(DateTime EndDate) => _dal.EndDate = EndDate;


    /************* CRUD functions ************/
    public int Create(BO.Task boTask)
    {
        if (boTask.Id < 0 || boTask.NickName == null || boTask.NickName == "")
            throw new BO.BlInvalidvalueException("one of the task's values is invalid");
        DO.Task doTask = new DO.Task
           (Id: boTask.Id,
            NickName: boTask.NickName,
            Description: boTask.Description ?? "",
            Deliverables: boTask.Deliverables,
            LevelOfDifficulty: (DO.EngineerExperience)boTask.LevelOfDifficulty!,
            Remarks: boTask.Remarks,
            DurationOfTask: boTask.DurationOfTask,
            DateOfCreation: DateTime.Now);

        try
        {
            int taskId = _dal.Task.Create(doTask);
            foreach (BO.TaskInList task in boTask.DependencyList!)
                _dal.Dependency.Create(new Dependency()
                { DependentTaskId = doTask.Id, DependentOnTaskId = task.Id });
            return taskId;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        }

    }

    public void Delete(int id)
    {
        if ((from dep in _dal.Dependency.ReadAll()
             let depOnTaskId = dep.DependentOnTaskId
             where depOnTaskId == id
             select dep).FirstOrDefault() != null)
            throw new BO.BlDeletionImpossible($"There are dependent tasks on Task with ID={id}");
        if (_dal.Task.Read(id) != null)//task exists
            try
            {
                _dal.Task.Delete(id);
            }
            catch (DO.DalDeletionImpossible ex)
            {
                throw new BO.BlDeletionImpossible($"Task with ID={id} does Not exist", ex);
            }
    }

    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        return ConvertTaskFromDOToBO(doTask);
    }

    public IEnumerable<BO.Task> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        return (from DO.Task doTask in _dal.Task.ReadAll(filter)
                select ConvertTaskFromDOToBO(doTask));
    }

    public void Update(BO.Task boTask)
    {//there are more fildes to update
        if (_dal.InitDate != null)
            throw new BlIncorrectDateOrder("can't update task after starting project");
        if (boTask.Id <= 0 || boTask.NickName == "" || boTask.NickName is null)
            throw new BO.BlInvalidvalueException("one of the task's values is invalid");

        DO.Task new_doTask = _dal.Task.Read(boTask.Id) ?? throw new BO.BlDoesNotExistException($"task with id= {boTask.Id} does not exist");
        if (new_doTask.EngineerId != null && boTask.EngineerId != new_doTask.EngineerId)//cheke that the task does'nt belong to another engineer
            throw new BO.BlCantSetValue($"task with id {boTask.Id} already has engineer");
        new_doTask = new_doTask with
        {
            NickName = boTask.NickName,
            Description = boTask.Description ?? "",
            Deliverables = boTask.Deliverables,
            LevelOfDifficulty = (DO.EngineerExperience)boTask.LevelOfDifficulty!,
            EngineerId = boTask.EngineerId,
            Remarks = boTask.Remarks,
            DurationOfTask = boTask.DurationOfTask,
            DateOfFinishing = EstimatedFinishingDate(boTask.Id),
            PlanedDateOfstratJob = CanAddOrUpdateDateOfTask(boTask.Id, boTask.PlanedDateOfstratJob)
                                ? boTask.PlanedDateOfstratJob
                                : throw new BO.BlIncorrectDateOrder($"The planed date of start job of task with id= {boTask.Id} is not valid"),
            DateOfstratJob = boTask.DateOfstratJob
        };


        try
        {
            _dal.Task.Update(new_doTask);
            IEnumerable<DO.Dependency> dependencies = _dal.Dependency.ReadAll(x => x.DependentTaskId == boTask.Id);//all the depending tasks
            if (boTask.DependencyList is null)
                boTask.DependencyList = new List<TaskInList>();//initialize as an empty list

            boTask.DependencyList.Where(_new => !dependencies.Any(old => old.DependentOnTaskId == _new.Id))
                .ToList().ForEach(dep => _dal.Dependency.Create(new DO.Dependency()
                {
                    DependentTaskId = boTask.Id,
                    DependentOnTaskId = dep.Id//the dapending task
                }));

            dependencies.Where(old => !boTask.DependencyList.Any(_new => _new.Id == old.DependentOnTaskId))
               .ToList().ForEach(dep => _dal.Dependency.Delete(dep.DependentOnTaskId));

        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} does Not exist", ex);
        }
    }


    /************ Tools  ************/

    public BO.Task ConvertTaskFromDOToBO(DO.Task doTask)
    {
        BO.EngineerInList? engOfTask = null;
        if (doTask.EngineerId is not null)
        {
            DO.Engineer doEngineer = _dal.Engineer.Read((int)doTask.EngineerId!)
                       ?? throw new BlDoesNotExistException($"engineer with id= {doTask.EngineerId} does not exist");
            engOfTask = new BO.EngineerInList()
            {
                Id = doEngineer.Id,
                Name = doEngineer.Name
            };
        }
        return new BO.Task()
        {
            Id = doTask.Id,
            NickName = doTask.NickName,
            Description = doTask.Description,
            Deliverables = doTask.Deliverables,
            Status = CheckStatus(doTask),
            LevelOfDifficulty = (BO.EngineerExperience)doTask.LevelOfDifficulty!,
            EngineerId = doTask.EngineerId,
            EngineerOfTask = engOfTask,
            Remarks = doTask.Remarks,
            DateOfCreation = doTask.DateOfCreation,
            PlanedDateOfstratJob = doTask.PlanedDateOfstratJob,
            DateOfstratJob = doTask.DateOfstratJob,
            DateOfFinishing = doTask.DateOfFinishing,
            DurationOfTask = doTask.DurationOfTask,
            EstimatedFinishingDate = EstimatedFinishingDate(doTask.Id),
            DependencyList = from DO.Dependency dep in _dal.Dependency.ReadAll(dep => dep.DependentTaskId == doTask.Id)
                             select TaskInListConvertor(_dal.Task.Read(dep.DependentOnTaskId)!)
        };
    }

    TaskInList TaskInListConvertor(DO.Task task)
    {
        return new BO.TaskInList()
        {
            Id = task.Id,
            NickName = task.NickName,
            Description = task.Description,
            Status = CheckStatus(task)
        };
    }

    private bool CanAddOrUpdateDateOfTask(int taskId, DateTime? taskDate)
    {
        if (taskDate == null) { return true; }
        if (taskDate < _dal.InitDate)//if the date is sooner then the projects starting date
            return false;
        IEnumerable<DO.Dependency> depenList = from dep in _dal.Dependency.ReadAll() where dep.DependentTaskId == taskId select dep;
        var v = (from DO.Dependency dep in depenList
                 let depTask = _dal.Task.Read(dep.DependentOnTaskId)!
                 where depTask.PlanedDateOfstratJob == null
                 select dep).FirstOrDefault();
        if (v != null)
            return false;
        v = (from DO.Dependency dep in depenList
             let depTask = _dal.Task.Read(dep.DependentOnTaskId)!
             where depTask.DateOfFinishing + depTask.DurationOfTask > taskDate
             select dep).FirstOrDefault();
        if (v != null)
            return false;
        return true;
    }

    private DateTime? EstimatedFinishingDate(int id)
    {
        DO.Task doTask = _dal.Task.Read(id)!;
        if (doTask.PlanedDateOfstratJob is null) return null;
        if (doTask.DateOfstratJob is null) return doTask.PlanedDateOfstratJob + doTask.DurationOfTask;

        DateTime? starting = doTask.PlanedDateOfstratJob > doTask.DateOfstratJob
                                ? doTask.PlanedDateOfstratJob : doTask.DateOfstratJob;
        return starting.Value + doTask.DurationOfTask;
    }

    private BO.Status CheckStatus(DO.Task doTask)
    {
        if (doTask.PlanedDateOfstratJob != null && doTask.DateOfstratJob == null)
            return BO.Status.Schedueled;
        if (doTask.DateOfstratJob != null && doTask.DateOfFinishing == null)//started to work and didn't finish
            return BO.Status.OnTrack;
        if (doTask.DateOfFinishing < DateTime.Now)
            return BO.Status.Done;
        //doTask.PlanedDateOfstratJob==null
        return BO.Status.Unschedueled;
    }

    //private BO.TaskInEngineer GetDetailedEngineerForTask(int EngineerId, int TaskId)
    //{
    //    throw new Exception();
    //}
}