namespace DO;
/// <summary>
/// Dependency Entity - represents a dependency with all its props
/// </summary>
/// </summary>
/// <param name="Id"></param>
/// <param name="DependentTaskId">< /param>
/// <param name="DependentOnTaskId">< /param>
/// 
public record Dependency
(
   int Id,///running identifier add something
   int? DependentTaskId=null,
    int? DependentOnTaskId=null
    )
{
    public Dependency() : this(0) { } //empty ctor for stage 3

}
