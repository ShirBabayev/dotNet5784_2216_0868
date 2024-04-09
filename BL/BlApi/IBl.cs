namespace BlApi;

public interface IBl
{
    public void InitializeDB();
    public void ResetDB();
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public DateTime Clock { get; set; }


}
