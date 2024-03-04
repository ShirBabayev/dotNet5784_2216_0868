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
            if (TaskId == 0) { tskString = "No current task"; }
        }
        return "Engineer id= " + EngineerId +
                            "\nEngineer name= " + Name +
                            "\nEngineer's level= " + Level +
                            "\n" + tskString +
                            "\n------------------------------";
    }
}
