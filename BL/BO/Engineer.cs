using DO;

namespace BO;
public class Engineer
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Email { get; set; }
    public  EngineerExperience Level { get; set; }
    public double Cost { get; set; }
    public TaskInEngineer? Task{ get; set; }  // public override string? ToString() => this.ToStringProperty();
    public override string ToString()
    {
        string? tskString = null;
        if (Task != null)
        {
            tskString = " Engineer's current task id= " + Task.Id;
        }
        else { Console.WriteLine(" no current task"); }
        return "Engineer id= " + Id +
                            " Engineer name= " + Name +
                            " Engineer's level= " + Level +
                            " Cost for engineer per an hour= " + Cost +
                            tskString;
    }
}
