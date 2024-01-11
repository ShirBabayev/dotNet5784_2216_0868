using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed public class DalList : IDal
{
    //public IEngineer Engineer => throw new NotImplementedException();

    //public ITask Task => throw new NotImplementedException();

    //public IDependency Dependency => throw new NotImplementedException();

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task =>  new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();
}
