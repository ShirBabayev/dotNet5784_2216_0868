namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        Engineer itemForChecking = DataSource.Engineers.Find(lk => lk.Id == item.Id);
        if (itemForChecking != null)
        {
            throw new Exception($"Engineer with ID={item.Id} already exists");
        }
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        Engineer item = DataSource.Engineers.Find(lk => lk.Id == id);
        if (item == null)
        {
            throw new Exception($"Engineer with ID={id} does not exists");
        }
        DataSource.Engineers.Remove(item);
    }

    public Engineer? Read(int id)
    {
        return DataSource.Engineers.Find(lk => lk.Id == id);
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Engineer itemToUpdate = DataSource.Engineers.Find(lk => lk.Id == item.Id);
        if (itemToUpdate == null)
        {
            throw new Exception($"Engineer with ID= {item.Id} does not exists");
        }
        DataSource.Engineers.Remove(itemToUpdate);
        DataSource.Engineers.Add(item);
    }
}
