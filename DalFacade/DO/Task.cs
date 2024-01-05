namespace DO;

public record Task
(       
        int Id,///running identifier add something
        string nickName,
        string description,
        bool mileStone,
        DateTime dateOfCreation,
        DateTime PlanedDateOfstratJob,
        DateTime dateOfstratJob,//the date when the mission was actually started
        TimeSpan durationOfTask,
        DateTime deadline,
        DateTime dateOfFinishing,//the date when the mission was actually done
        string deliverables,//products???
        int engineerId,
        int levelOfDifficulty
    )
{

}
