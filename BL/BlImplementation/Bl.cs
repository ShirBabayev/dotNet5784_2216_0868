namespace BlImplementation;
using BlApi;
internal class Bl : IBl
{
    public void InitializeDB() => DalTest.Initialization.Do();
    public void ResetDB() => DalTest.Initialization.Reset();
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();
}
