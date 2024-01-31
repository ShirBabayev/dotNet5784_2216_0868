namespace BlApi;
/// <summary>
/// ITask
/// </summary>
public interface ITask
{
    public int Create(BO.Task item);
    public BO.Task? Read(int id);
    public IEnumerable<BO.TaskInList> ReadAll();
    public BO.Status CheckStatus(DO.Task doTask);
    public DateTime EstimatedFinishingDate(DO.Task doTask);

    public void Update(BO.Task item);
    public void Delete(int id);
    public BO.TaskInEngineer GetDetailedEngineerForTask(int EngineerId, int TaskId);
}
