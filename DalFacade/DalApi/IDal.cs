namespace DalApi;

/// <summary>
/// 
/// </summary>
public interface IDal
{
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
