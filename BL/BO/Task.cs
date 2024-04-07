namespace BO;

public class Task
{
    public int Id { get; init; }
    public string? NickName { get; set; }
    public string? Description { get; set; }
    //public bool MileStone { get; set; }
    public BO.Status Status { get; init; }//////////////////////////

    public string? Deliverables { get; set; }
    public EngineerExperience? LevelOfDifficulty { get; set; }
    public int? EngineerId { get; set; }
    public EngineerInList? EngineerOfTask { get; set; }//////////////////////////
    public string? Remarks { get; set; }
    public DateTime DateOfCreation { get; init; }
    public DateTime? PlanedDateOfstratJob { get; set; }
    public DateTime? DateOfstratJob { get; set; }
    public TimeSpan? DurationOfTask { get; set; }
    //public DateTime? Deadline  { get; set; }
    public DateTime? DateOfFinishing { get; set; }
    //public override string? ToString() => this.ToStringProperty();
    public IEnumerable<TaskInList>? DependencyList { get; set; }//////////////////////////
    public DateTime? EstimatedFinishingDate { get; set; }//////////////////////////
    public override string ToString()
    {
        return "task id= " + Id + " task Nick name= " + NickName;
    }
}