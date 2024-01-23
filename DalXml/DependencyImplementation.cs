//using DalApi;
//using DO;
//using System;
//using System.Xml;
//using System.Xml.Linq;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Collections;
//using static DO.Dependency;

//namespace Dal;
//internal class DependencyImplementation : IDependency
//{
//    readonly string s_dependency_xml = "dependencies";
//    public int Create(Dependency item)
//    {
//        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependency_xml);
//        //List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependency_xml);
//        int newDependencyId = XMLTools.GetAndIncreaseNextId("Config", s_dependency_xml);//next id number config func
//        dependencies.Add(item with { Id = newDependencyId });
//        XMLTools.SaveListToXMLElement(dependencies, s_dependency_xml);
//        //XMLTools.SaveListToXMLSerializer(dependencies, s_dependency_xml);
//        return item.Id;
//        //throw new NotImplementedException();
//    }

//    public void Delete(int id)
//    {
//        XmlSample xml = new XmlSample();
//        List<Dependency> dependencies = xml.GetDependencyList();
//        // Dependency item = xml.GetDepenency(id)
//        // ?? throw new DalDoesNotExistException($"Dependency with ID={id} does not exists");
//        xml.RemoveDependency(id);
//        XMLTools.SaveListToXMLElement(dependencies, s_dependency_xml);
//        //throw new NotImplementedException();
//    }

//    public Dependency? Read(int id)
//    {
//        XmlSample xml = new XmlSample();
//        XElement? dependencyElem = XMLTools.LoadListFromXMLElement(s_dependency_xml).Elements().FirstOrDefault(dependency => (int?)dependency.Element("Id") == id);
//        return dependencyElem is null ? null : xml.GetDepenency(id);
//        /*dependencies.FirstOrDefault(dependency => dependency.Id == id);*/
//        //throw new NotImplementedException();
//    }

//    public Dependency? Read(Func<Dependency, bool> filter)
//    {
//        XmlSample xml = new XmlSample();
//        XElement? dependencyElem = XMLTools.LoadListFromXMLElement(s_dependency_xml).Elements().Select(dependency => xml.GetDepenency(dependency)).FirstOrDefault(filter);
//        //List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependency_xml);
//        //return dependencies.FirstOrDefault(filter);
//        return dependencyElem is null ? null : xml.GetDepenency();
//        //throw new NotImplementedException();
//    }
//    //dependencies.Elements()
//    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null)
//    {
//        XmlSample xml = new XmlSample();
//        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependency_xml);
//        return filter == null ? xml.GetDependencyList() : xml.GetDependencyList().Where(filter);//?????
//    }


//    public void Update(Dependency item)
//    {
//        XmlSample xml = new XmlSample();
//        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependency_xml);
//        Dependency itemToUpdate = xml.GetDepenency(item.Id)
//           ?? throw new DalDoesNotExistException($"Dependency with ID= {item.Id} does not exists");
//        xml.RemoveDependency(item.Id);
//        dependencies.Add(item);
//        XMLTools.SaveListToXMLElement(dependencies, s_dependency_xml);
//        //throw new NotImplementedException();
//    }
//}














using DalApi;
using DO;
using System;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.Common;
using System.Collections;
using static DO.Dependency;
using System.Linq;

namespace Dal;
internal class DependencyImplementation : IDependency
{
    readonly string s_dependency_xml = "dependencies";
    static Dependency getDependency(XElement d)
    {
        return new Dependency()
        {
            Id = d.ToIntNullable("Id") ?? throw new FormatException("can't convert id"),
            DependentTaskId = d.ToIntNullable("Id") ?? throw new FormatException("can't convert id"),
            DependentOnTaskId = d.ToIntNullable("Id") ?? throw new FormatException("can't convert id")
        };
    }
    public int Create(Dependency item)
    {
        List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependency_xml);//Deserialize
        int newId = XMLTools.GetAndIncreaseNextId("Config", s_dependency_xml);//next id number config func
        Dependency newItem = item with { Id = newId };
        dependencies.Add(newItem);
        XMLTools.SaveListToXMLSerializer(dependencies, s_dependency_xml);//Serialize
        return item.Id;
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependency_xml);//Deserialize
        Dependency item = dependencies.Find(lk => lk.Id == id)
          ?? throw new DalDoesNotExistException($"Dependency with ID={id} does not exists");
        dependencies.Remove(item);
        XMLTools.SaveListToXMLSerializer(dependencies, s_dependency_xml);//Serialize
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependency_xml);//Deserialize
        return dependencies.FirstOrDefault(dependency => dependency.Id == id);
        // return dependencies.Find(lk => lk.Id == id);
        throw new NotImplementedException();
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependency_xml);//Deserialize
        return dependencies.FirstOrDefault(filter);
        throw new NotImplementedException();
    }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null)
    {
        List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependency_xml);//Deserialize
        return filter == null ? dependencies.Select(item => item)
                              : dependencies.Where(filter);
    }
    public void Update(Dependency item)
    {
        List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependency_xml);//Deserialize
        Dependency itemToUpdate = dependencies.Find(lk => lk.Id == item.Id)
           ?? throw new DalDoesNotExistException($"Dependency with ID= {item.Id} does not exists");
        dependencies.Remove(itemToUpdate);
        dependencies.Add(item);
        XMLTools.SaveListToXMLSerializer(dependencies, s_dependency_xml);//Serialize
        throw new NotImplementedException();
    }
}
