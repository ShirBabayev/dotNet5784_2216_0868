namespace DO;

public record Task
(
        int Id,///running identifier add something
        string nickName,
        string description,
        bool mileStone,
        string deliverables,//products???
        DifficultyLevel levelOfDifficulty,
        int engineerId,
        DateTime? dateOfCreation = null,
        DateTime? PlanedDateOfstratJob = null,
        DateTime? dateOfstratJob = null,//the date when the mission was actually started
        TimeSpan? durationOfTask = null,
        DateTime? deadline = null,
        DateTime? dateOfFinishing = null//the date when the mission was actually done
    )
{
    public Task(int Id, string nickName, string description,
        bool mileStone, DateTime dateOfCreation, DateTime PlanedDateOfstratJob, DateTime dateOfstratJob,
        TimeSpan durationOfTask, DateTime deadline, DateTime dateOfFinishing, string deliverables,
        int engineerId, DifficultyLevel levelOfDifficulty) : this()
    {
        this.Id=Id;
        this.nickName=nickName;
        this.description=description;
        this.mileStone=mileStone;
        this.dateOfCreation=dateOfCreation;
        this.PlanedDateOfstratJob=PlanedDateOfstratJob;
        this.dateOfstratJob=dateOfstratJob;
        this.durationOfTask=durationOfTask;
        this.deadline=deadline;
        this.dateOfFinishing=dateOfFinishing;
        this.deliverables=deliverables;
        this.engineerId=engineerId;
        this.levelOfDifficulty=levelOfDifficulty;
    }

    public Task() : this(0, null, null, false, null, 0, 0) { } //empty ctor for stage 3
}