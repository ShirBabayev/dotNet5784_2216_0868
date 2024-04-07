namespace DO;

/// <summary>
///  Task Entity - represents a task with all its props
/// </summary>
/// <param name="Id"> a number that identifies the task </param>
/// <param name="NickName"> the name of the task </param>
/// <param name="Description"> the description of the task </param>
///// <param name="MileStone">  </param>
/// <param name="Deliverables"> the results or the deliverables that are produced in the task </param>
/// <param name="LevelOfDifficulty"> defines the minimum engineer's expertise that can work on this task  </param>
/// <param name="EngineerId"> the ID of the engineer that works on this task </param>
/// <param name="Remarks"> optional to write notes about the task </param>
/// <param name="DateOfCreation"> represents the time when the manager created the task </param>
/// <param name="PlanedDateOfStartJob"> the time that it takes to make a task - calculated in the schedule creation </param>
/// <param name="DateOfStartJob"> when the engineer actually starts working on this task </param>
/// <param name="DurationOfTask"> how many days are required to finish this task </param>
///// <param name="Deadline"> The latest possible date on which the completion of the task will not cause the project to fail </param>
/// <param name="DateOfFinishing"> When an engineer reports that he has finished working on the task </param>
public record Task
(
    int Id,///running identifier add something
    string NickName,
    string Description,
    //bool MileStone = false,
    DateTime DateOfCreation,
    string? Deliverables = null,//products
    EngineerExperience? LevelOfDifficulty = null,
    int? EngineerId = null,
    string? Remarks = null,
    DateTime? PlanedDateOfstratJob = null,
    DateTime? DateOfstratJob = null,
    TimeSpan? DurationOfTask = null,
    //DateTime? Deadline = null,
    DateTime? DateOfFinishing = null
)
{
    public Task() : this(0, "", "", DateTime.Now) { } //empty ctor for stage 3
}
