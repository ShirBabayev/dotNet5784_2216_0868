namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
//using System.Linq;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        Engineer itemForChecking = DataSource.Engineers.Find(lk => lk.Id == item.Id);
        if (itemForChecking != null)
        {
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        }
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        Engineer item = DataSource.Engineers.Find(lk => lk.Id == id);
        if (item == null)
        {
            throw new DalDoesNotExistException($"Engineer with ID={id} does not exists");
        }
        DataSource.Engineers.Remove(item);
    }

    public Engineer? Read(int id)
    {
        return DataSource.Engineers.FirstOrDefault(engineer => engineer.Id == id);
        //return DataSource.Engineers.Find(lk => lk.Id == id);
    }

    //public List<Engineer> ReadAll()
    //{
    //    return DataSource.Engineers/*.ToList()*/;
    //    //return new List<Engineer>(DataSource.Engineers);
    //}
    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else
            return DataSource.Engineers.Where(filter);
    }
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers.FirstOrDefault(filter);
    }

    public void Update(Engineer item)
    {
        Engineer itemToUpdate = DataSource.Engineers.Find(lk => lk.Id == item.Id);
        if (itemToUpdate == null)
        {
            throw new DalDoesNotExistException($"Engineer with ID= {item.Id} does not exists");
        }
        DataSource.Engineers.Remove(itemToUpdate);
        DataSource.Engineers.Add(item);
    }
}
