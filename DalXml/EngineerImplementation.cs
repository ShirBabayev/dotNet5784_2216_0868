namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

internal class EngineerImplementation : IEngineer
{
    readonly string s_engineer_xml = "engineers";
    public int Create(Engineer item)
    {
        List<DO.Engineer> engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>(s_engineer_xml);//Deserialize
        Engineer itemForChecking = engineers.Find(lk => lk.Id == item.Id)!;
        if(itemForChecking!=null) throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        engineers.Add(item);
        XMLTools.SaveListToXMLSerializer(engineers, s_engineer_xml);//Serialize
        return item.Id;
    }

    public void Delete(int id)
    {
        List<DO.Engineer> engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>(s_engineer_xml);//Deserialize
        Engineer item = engineers.Find(lk => lk.Id == id)
            ?? throw new DalDoesNotExistException($"Engineer with ID={id} does not exists");
        engineers.Remove(item);
        XMLTools.SaveListToXMLSerializer(engineers, s_engineer_xml);//Serialize
    }

    public Engineer? Read(int id)
    {
        List<DO.Engineer> engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>(s_engineer_xml);//Deserialize
        return engineers.FirstOrDefault(engineer => engineer.Id == id);
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<DO.Engineer> engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>(s_engineer_xml);//Deserialize
        return engineers.FirstOrDefault(filter);
    }

    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<DO.Engineer> engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>(s_engineer_xml);//Deserialize
        return filter == null ? engineers.Select(item => item)
                              : engineers.Where(filter);
    }
    

    public void Update(Engineer item)
    {
        List<DO.Engineer> engineers = XMLTools.LoadListFromXMLSerializer<DO.Engineer>(s_engineer_xml);//Deserialize
        Engineer itemToUpdate = engineers.Find(lk => lk.Id == item.Id)
           ?? throw new DalDoesNotExistException($"Engineer with ID= {item.Id} does not exists");
        engineers.Remove(itemToUpdate);
        engineers.Add(item);
        XMLTools.SaveListToXMLSerializer(engineers, s_engineer_xml);//Serialize
    }
}
