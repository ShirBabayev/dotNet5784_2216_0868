namespace DO;

public record Dependency
(
   int Id,///running identifier add something
   int? dependentTaskId=null,
    int? formerTaskId=null
    )
{
    public Dependency(int id, int dependentTaskId, int formerTaskId) : this()
    {
        this.Id = id;
        this.dependentTaskId = dependentTaskId;
        this.formerTaskId = formerTaskId;
    }

    public Dependency() : this(0) { } //empty ctor for stage 3

}
