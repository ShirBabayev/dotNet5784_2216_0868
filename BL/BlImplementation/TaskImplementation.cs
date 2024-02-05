using BlApi;
using BO;
using DalApi;
using DO;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BlImplementation;

internal class TaskImplementation : BlApi.ITask
{
    private DalApi.IDal _dal = Factory.Get;
    public int Create(BO.Task boTask)
    {
        if (boTask.Id<=0||boTask.NickName=="")
            throw new BO.BlInvalidvalueException("one of the task's values is invalid");
        DO.Task doTask = new DO.Task
           (Id: boTask.Id,
            NickName: boTask.NickName,
            Description: boTask.Description,
            Deliverables: boTask.Deliverables,
            LevelOfDifficulty: (DO.EngineerExperience)boTask.LevelOfDifficulty!,
            Remarks: boTask.Remarks,
            DurationOfTask: boTask.DurationOfTask,
            DateOfCreation: boTask.DateOfCreation);
        try
        {
            int taskId = _dal.Task.Create(doTask);
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
        if (_dal.Task.Read(id)!=null)//task exists
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
        return new BO.Task()
        {
            Id = doTask.Id,
            NickName = doTask.NickName,
            Description = doTask.Description,
            Deliverables = doTask.Deliverables,
            Status = CheckStatus(doTask),
            LevelOfDifficulty = (BO.EngineerExperience)doTask.LevelOfDifficulty!,
            EngineerId = doTask.EngineerId,
            EngineerOfTask = new BO.EngineerInList()
            {
                Id = doTask.Id,
                Name = _dal.Engineer.Read(id)!.Name
            },
            Remarks = doTask.Remarks,
            DateOfCreation = doTask.DateOfCreation,
            PlanedDateOfstratJob = doTask.PlanedDateOfstratJob,
            DateOfstratJob = doTask.DateOfstratJob,
            DateOfFinishing = doTask.DateOfFinishing,
            DurationOfTask = doTask.DurationOfTask,
            EstimatedFinishingDate = EstimatedFinishingDate(doTask.Id),
            DependncyList = from DO.Dependency dep in _dal.Dependency.ReadAll()
                            let DependentOnTask = _dal.Task.Read(dep.DependentOnTaskId)
                            where dep.DependentTaskId == id//check if i can send it in the readAll
                            select TaskInListCalc(DependentOnTask)
        };
    }
    public TaskInList TaskInListCalc(DO.Task task)
    {
        return new BO.TaskInList()
        {
            Id = task.Id,
            NickName = task.NickName,
            Description = task.Description,
            Status = CheckStatus(task)
        };
    }
    //public IEnumerable<BO.TaskInList> ReadAll()
    //{
    //    return (from DO.Task doTask in _dal.Task.ReadAll()
    //            select new BO.TaskInList
    //            {
    //                Id = doTask.Id,
    //                NickName = doTask.NickName,
    //                Description = doTask.Description,
    //                Status=CheckStatus(doTask)
    //            });
    //}
    
    public IEnumerable<BO.TaskInList> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        return (from DO.Task doTask in _dal.Task.ReadAll(filter)
                select new BO.TaskInList
                {
                    Id = doTask.Id,
                    NickName = doTask.NickName,
                    Description = doTask.Description,
                    Status=CheckStatus(doTask)
                });
    }
    public void Update(BO.Task boTask)
    {//there are more fildes to update
        if (boTask.Id<=0||boTask.NickName=="")
            throw new BO.BlInvalidvalueException("one of the task's values is invalid");
        DO.Task? new_doTask = new DO.Task
             (Id: boTask.Id,
            NickName: boTask.NickName,
            Description: boTask.Description,
            Deliverables: boTask.Deliverables,
            LevelOfDifficulty: (DO.EngineerExperience)boTask.LevelOfDifficulty!,
            EngineerId: boTask.EngineerId,
            Remarks: boTask.Remarks,
            DateOfCreation: boTask.DateOfCreation,
            DurationOfTask: boTask.DurationOfTask,
            DateOfFinishing: EstimatedFinishingDate(boTask.Id),
            DateOfstratJob: CanAddOrUpdateDateOfTask(boTask.Id, boTask.DateOfstratJob) ? boTask.DateOfstratJob : throw new Exception());
        try
        {
            _dal.Task.Update(new_doTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} does Not exist", ex);
        }
    }

    public bool CanAddOrUpdateDateOfTask(int taskId, DateTime? taskDate)
    {
        IEnumerable<DO.Dependency> depenList = from dep in _dal.Dependency.ReadAll() where dep.DependentTaskId == taskId select dep;
        var v = (from DO.Dependency dep in depenList
                 let depTask = _dal.Task.Read(dep.DependentOnTaskId)!
                 where depTask.PlanedDateOfstratJob == null
                 select dep).FirstOrDefault();
        if (v != null)
            return false;
        v = (from DO.Dependency dep in depenList
             let depTask = _dal.Task.Read(dep.DependentOnTaskId)!
             where depTask.DateOfFinishing+depTask.DurationOfTask > taskDate
             select dep).FirstOrDefault();
        if (v != null)
            return false;
        return true;
    }
    public DateTime EstimatedFinishingDate(int id /*DO.Task doTask*/)
    {
        DO.Task doTask = _dal.Task.Read(id)!;
        DateTime? starting = doTask.PlanedDateOfstratJob > doTask.DateOfstratJob
        ? doTask.PlanedDateOfstratJob : doTask.DateOfstratJob;
        return starting!.Value +(TimeSpan)(doTask.DurationOfTask!);
    }
    public BO.Status CheckStatus(DO.Task doTask)
    {
        if (doTask.PlanedDateOfstratJob!=null&&doTask.DateOfstratJob==null)
            return BO.Status.Schedueled;
        if (doTask.DateOfstratJob!=null&&doTask.DateOfFinishing==null)//started to work and didn't finish
            return BO.Status.OnTrack;
        if (doTask.DateOfFinishing<DateTime.Now)
            return BO.Status.Done;
        //doTask.PlanedDateOfstratJob==null
        return BO.Status.Unschedueled;
    }
    public BO.TaskInEngineer GetDetailedEngineerForTask(int EngineerId, int TaskId)
    {
        throw new Exception();
    }
}