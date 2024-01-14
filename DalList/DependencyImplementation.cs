﻿namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
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
            throw new DalDoesNotExistException($"Dependency with ID={id} does not exists");
        }
        DataSource.Dependencies.Remove(item);
    }

    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.FirstOrDefault(dependency => dependency.Id == id);
        //return DataSource.Dependencies.Find(lk => lk.Id == id );
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencies.FirstOrDefault(filter);
    }
    public IEnumerable<Dependency?> ReadAll(Func<Dependency?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Dependencies.Select(item => item);
        else
            return DataSource.Dependencies.Where(filter);
    }

    public void Update(Dependency item)
    {
        Dependency itemToUpdate = DataSource.Dependencies.Find(lk => lk.Id == item.Id);
        if (itemToUpdate == null)
        {
            throw new DalDoesNotExistException($"Dependency with ID= {item.Id} does not exists");
        }
        DataSource.Dependencies.Remove(itemToUpdate);
        DataSource.Dependencies.Add(item);
    }
}
