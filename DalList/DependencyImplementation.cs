namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int newDependencyId = DataSource.Config.NextDependencyId;
        DataSource.Dependencies.Add(item with { Id= newDependencyId });
        return item.Id;
    }

    public void Delete(int id)
    {
        Dependency item=DataSource.Dependencies.Find(lk => lk.Id == id);
        if (item == null)
        {
            throw new Exception($"Dependency with ID={id} does not exists");
        }
        DataSource.Dependencies.Remove(item);
    }

    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.Find(lk => lk.Id == id );
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency item)
    {
        Dependency itemToUpdate = DataSource.Dependencies.Find(lk => lk.Id == item.Id);
        if (itemToUpdate == null)
        {
            throw new Exception($"Dependency with ID= {item.Id} does not exists");
        }
        DataSource.Dependencies.Remove(itemToUpdate);
        DataSource.Dependencies.Add(item);
    }
}
