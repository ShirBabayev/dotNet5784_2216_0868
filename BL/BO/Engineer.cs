using DO;
using System.Threading.Tasks;

namespace BO;
public class Engineer
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Email { get; set; }
    public  EngineerExperience Level { get; set; }
    public double Cost { get; set; }
    public TaskInEngineer? Task{ get; set; } 
    public override string ToString()
    {
        string? tskString = null;
        if (Task != null)
        {
            tskString = " Engineer's current task id= " + Task.Id;
            if (Task.Id == 0) { tskString = "No current task"; }
        }
        else tskString = "No current task";


        return "Engineer id= " + Id +
                            "\nEngineer name= " + Name +
                            "\nEngineer's level= " + Level +
                            "\nCost for engineer per an hour= " + Cost +
                            "\n" + tskString +
                            "\n------------------------------";
    }
}
