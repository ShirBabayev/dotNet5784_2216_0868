using DalApi;
using System;
using System.Xml;
using System.Xml.Linq;

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
   int Id,///running identifier
   int DependentTaskId,
   int DependentOnTaskId
    )
{
    public Dependency() : this(0,0,0) { } //empty ctor
}