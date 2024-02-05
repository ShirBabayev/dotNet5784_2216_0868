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
        DO.Task doTask = new DO.Task
           (Id: 0,
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
        if (_dal.Task.Read(id)!=null            
            && (from dep in _dal.Dependency.ReadAll()
            let depOnTaskId = dep.DependentOnTaskId
            where depOnTaskId == id
            select dep).FirstOrDefault() == null)
        try
        {
            _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={id} does Not exist", ex);
        }
    }

    public BO.TaskInEngineer GetDetailedEngineerForTask(int EngineerId, int TaskId)
    {
        throw new Exception();
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
            DependncyList = from DO.Dependency dep in _dal.Dependency.ReadAll()
                            let DependentTask = _dal.Task.Read(dep.DependentTaskId)
                            where dep.DependentTaskId == id
                            select new BO.TaskInList() {
                                Id = DependentTask.Id,
                                NickName = DependentTask.NickName,
                                Description = DependentTask.Description,
                                Status = CheckStatus(DependentTask) },
            EstimatedFinishingDate = EstimatedFinishingDate(doTask.Id)
        };

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
    public IEnumerable<BO.TaskInList> ReadAll()
    {
        return (from DO.Task doTask in _dal.Task.ReadAll()
                select new BO.TaskInList
                {
                    Id = doTask.Id,
                    NickName = doTask.NickName,
                    Description = doTask.Description,
                    Status=CheckStatus(doTask)
                });
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

    public void Update(BO.Task boTask)
    {//there are more fildes to update
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
}
