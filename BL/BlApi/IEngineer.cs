using BO;

namespace BlApi;
/// <summary>
/// IEngineer
/// </summary>
public interface IEngineer
{
    public int Create(BO.Engineer item);
    public BO.Engineer? Read(int id);
    //public IEnumerable<BO.EngineerInTask> ReadAll(/*Func<BO.Engineer, bool>? filter = null*/);
    public IEnumerable<BO.EngineerInTask> ReadAll(Func<DO.Engineer, bool>? filter = null);

    public void Update(BO.Engineer item);
    public void Delete(int id);
    public BO.TaskInEngineer CheckTaskForEngineer(int engineerId, int taskId);
    public bool IsCurrentTask(int engId, int taskId);
    public bool IsValidEmail(string email);
    public IEnumerable<BO.TaskInEngineer> GetDetailedTaskForEngineer(int EngineerId);

}
