namespace DO;

public record Dependency
(
   int Id,///running identifier add something
   int? dependentTaskId=null,
    int? formerTaskId=null
    )
{
    public Dependency() : this(0) { } //empty ctor for stage 3

}
