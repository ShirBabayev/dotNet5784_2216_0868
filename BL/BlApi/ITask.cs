using BO;

namespace BlApi;
/// <summary>
/// ITask
/// </summary>
public interface ITask
{
    public int Create(BO.Task item);
    public BO.Task? Read(int id);
    public IEnumerable<BO.Task> ReadAll(Func<DO.Task, bool>? filter = null);
    public BO.Status CheckStatus(DO.Task doTask);
    public DateTime EstimatedFinishingDate(int id /*DO.Task doTask*/);
    public bool CanAddOrUpdateDateOfTask(int taskId, DateTime? taskDate);
    public TaskInList TaskInListConvertor(DO.Task task);
    public BO.Task ConvertTaskFromDOToBO(DO.Task doTask);

    public void Update(BO.Task item);
    public void Delete(int id);
    public BO.TaskInEngineer GetDetailedEngineerForTask(int EngineerId, int TaskId);
}
