namespace BlApi;
/// <summary>
/// IEngineer
/// </summary>
public interface IEngineer
{
    public int Create(BO.Engineer item);
    public BO.Engineer? Read(int id);
    public IEnumerable<BO.EngineerInList> ReadAll();
    public void Update(BO.Engineer item);
    public void Delete(int id);
    public BO.EngineerInTask GetDetailedTaskForEngineer(int EngineerId, int TaskId);
}
