namespace DalApi;

/// <summary>
/// 
/// </summary>
public interface IDal
{
    DateTime? InitDate { get; set; }    
    DateTime? EndDate { get; set; }  

    /// <summary>
    /// 
    /// </summary>
    IEngineer Engineer { get; }

    /// <summary>
    /// 
    /// </summary>
    ITask Task { get; }

    /// <summary>
    /// 
    /// </summary>
    IDependency Dependency { get; }
}
