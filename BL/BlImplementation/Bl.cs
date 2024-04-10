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

    public DateTime? InitDate
    {
        get => dal.InitDate;
        set => dal.InitDate = value;
    }
    public DateTime? EndDate
    {
        get => dal.EndDate;
        set => dal.EndDate = value;
    }

}
