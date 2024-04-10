using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalXml : IDal
{
    /////////////////////////////////////////////////////////////////////////

    public DateTime? InitDate { get; set;}

    public DateTime? EndDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DateTime Clock { get; set; } = DateTime.Now;

    /////////////////////////////////////////////////////////////////////////
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }

    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();
    public IDependency Dependency => new DependencyImplementation();

    }
