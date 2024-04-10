namespace BO;

public class Task
{
    public int Id { get; init; }
    public string? NickName { get; set; }
    public string? Description { get; set; }
    public BO.Status Status { get; init; }// the status of the current task if someone is working on it etc

    public string? Deliverables { get; set; }
    public EngineerExperience? LevelOfDifficulty { get; set; }
    public int? EngineerId { get; set; }
    public EngineerInList? EngineerOfTask { get; set; }//the engineer that is set for this task
    public string? Remarks { get; set; }
    public DateTime DateOfCreation { get; init; }
    public DateTime? PlanedDateOfstratJob { get; set; }
    public DateTime? DateOfstratJob { get; set; }
    public TimeSpan? DurationOfTask { get; set; }
    //public DateTime? Deadline  { get; set; }
    public DateTime? DateOfFinishing { get; set; }
    public IEnumerable<TaskInList>? DependencyList { get; set; }// a list of the tasks that this task depends on them
    public DateTime? EstimatedFinishingDate { get; set; }// a calculated filed for the prdicted finishing date
    public override string ToString()
    {
        return "Task with Id: " + Id + " Nick name: " + NickName + " Level: " + LevelOfDifficulty;
    }
}