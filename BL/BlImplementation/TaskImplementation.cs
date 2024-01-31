using BlApi;
using BO;
using DalApi;
using DO;
using System.Threading.Tasks;

namespace BlImplementation;

internal class TaskImplementation : ITask
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
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        return new BO.Task()
        {
            Id= doTask.Id,
            NickName= doTask.NickName,
            Description= doTask.Description,
            Deliverables= doTask.Deliverables,
            Status=CheckStatus(doTask),
            LevelOfDifficulty= (BO.EngineerExperience)doTask.LevelOfDifficulty!,
            EngineerOfTask=new BO.EngineerInTask()
            {
                EngineerId = doTask.EngineerId,
                TaskId=doTask.Id

            },
            Remarks= doTask.Remarks,
            DurationOfTask= doTask.DurationOfTask,
            DateOfCreation= doTask.DateOfCreation,
            EstimatedFinishingDate = EstimatedFinishingDate(doTask)
        };

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
    public DateTime EstimatedFinishingDate(DO.Task doTask)
    {
        DateTime? starting = doTask.PlanedDateOfstratJob > doTask.DateOfstratJob
        ? doTask.PlanedDateOfstratJob : doTask.DateOfstratJob;
        return starting!.Value +(TimeSpan)(doTask.DurationOfTask!);
    }
    public BO.Status CheckStatus(DO.Task doTask)
    {
        if (doTask.PlanedDateOfstratJob!=null&&doTask.DateOfstratJob==null)
            return Status.Schedueled;
        if (doTask.DateOfstratJob!=null&&doTask.DateOfFinishing==null)//started to work and didn't finish
            return Status.OnTrack;
        if (doTask.DateOfFinishing<DateTime.Now)
            return Status.Done;
        //doTask.PlanedDateOfstratJob==null
        return Status.Unschedueled;
    }

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }
}
