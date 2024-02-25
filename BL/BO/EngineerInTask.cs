using DO;

namespace BO;

public class EngineerInTask
{
    public int EngineerId {  get; init; }
    public string? Name { get; init; }
    public EngineerExperience Level { get; init; }
    public int? TaskId { get; init; }
    public override string ToString()
    {
        string? tskString = null;
        if (TaskId != null)
        {
            tskString = " Engineer's current task id= " + TaskId;
        }
        else { Console.WriteLine(" no current task"); }
        return "Engineer id= " + EngineerId +
                            " Engineer name= " + Name +
                            " Engineer's level= " + Level +
                            tskString;
    }
}
