namespace BlImplementation;
using BlApi;
internal class Bl : IBl
{
    DalApi.IDal dal = DalApi.Factory.Get;
    public void InitializeDB() => DalTest.Initialization.Do();
    public void ResetDB() => DalTest.Initialization.Reset();
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();
    public DateTime Clock
    {
        get => dal.Clock;
        set => dal.Clock = value;
    }
}
