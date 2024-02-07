using BO;
using DalApi;

namespace BlApi;
/// <summary>
/// ITask
/// </summary>
public interface ITask
{
    public void StartProject(DateTime dateTime);
    public void EndProject(DateTime dateTime);
    public int Create(BO.Task item);
    public BO.Task? Read(int id);
    public IEnumerable<BO.Task> ReadAll(Func<DO.Task, bool>? filter = null);
    public void Update(BO.Task item);
    public void Delete(int id);
}
