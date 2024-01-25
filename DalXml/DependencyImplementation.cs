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
    readonly string s_data_config_xml = "data-config";

    static Dependency getDependency(XElement d)
    {
        return new Dependency()
        {
            Id = d.ToIntNullable("Id") ?? throw new FormatException("can't convert id"),
            DependentTaskId = d.ToIntNullable("DependentTaskId") ?? throw new FormatException("can't convert dependent task id"),
            DependentOnTaskId = d.ToIntNullable("DependentOnTaskId") ?? throw new FormatException("can't convert dependent on task id")
        };
    }
    public int Create(Dependency item)
    {
        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependency_xml);
        //List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependency_xml);//Deserialize
        int newId = XMLTools.GetAndIncreaseNextId(s_data_config_xml, "nextTaskId");//next id number config func
        Dependency newItem = item with { Id = newId };
        dependencies.Add(newItem);
        XMLTools.SaveListToXMLElement(dependencies, s_dependency_xml);
        //XMLTools.SaveListToXMLSerializer(dependencies, s_dependency_xml);//Serialize
        return item.Id;
    }

    public void Delete(int id)
    {
        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependency_xml);
        //XElement dependencyItem = dependencies.Elements().FirstOrDefault(dependency => (int?)dependency.Element("Id") == item.Id)!;
        //List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependency_xml);//Deserialize
        XElement dependencyItem = XMLTools.LoadListFromXMLElement(s_dependency_xml).Elements().FirstOrDefault(dependency => (int?)dependency.Element("Id") == id)!
            ?? throw new DalDoesNotExistException($"Dependency with ID={id} does not exists");
        dependencyItem.Remove();
        XMLTools.SaveListToXMLElement(dependencies, s_dependency_xml);
        //XMLTools.SaveListToXMLSerializer(dependencies, s_dependency_xml);//Serialize
    }

    public Dependency? Read(int id)
    {
        XElement dependencyItem = XMLTools.LoadListFromXMLElement(s_dependency_xml).Elements().FirstOrDefault(dependency => (int?)dependency.Element("Id") == id)!;
        return dependencyItem is null ? null : getDependency(dependencyItem);

        //List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependency_xml);//Deserialize
        //return dependencies.FirstOrDefault(dependency => dependency.Id == id);//
        // return dependencies.Find(lk => lk.Id == id);
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        //XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependency_xml);
        return XMLTools.LoadListFromXMLElement(s_dependency_xml).Elements().Select(Dependency => getDependency(Dependency)).FirstOrDefault(filter);
        //List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependency_xml);//Deserialize
        //return dependencies.FirstOrDefault(filter);
    }
    
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null)
    {
        //List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependency_xml);//Deserialize
        return filter == null ? XMLTools.LoadListFromXMLElement(s_dependency_xml).Elements().Select(Dependency => getDependency(Dependency))
                              : XMLTools.LoadListFromXMLElement(s_dependency_xml).Elements().Select(Dependency => getDependency(Dependency)).Where(filter);
                              //return filter == null ? dependencies.Select(item => item)
                              //: dependencies.Where(filter);
    }
    public void Update(Dependency item)
    {
        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependency_xml);
        XElement dependencyItem = dependencies.Elements().FirstOrDefault(dependency => (int?)dependency.Element("Id") == item.Id)!;
        //Dependency itemToUpdate = getDependency(dependencyItem);
        if (dependencyItem != null) 
        {
            dependencyItem.Remove();
            dependencies.Add(item);
        }
        else
            throw new DalDoesNotExistException($"Dependency with ID= {item.Id} does not exists");
        XMLTools.SaveListToXMLElement(dependencies, s_dependency_xml);
       // //XmlSample xml = new XmlSample();
       // //List<DO.Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<DO.Dependency>(s_dependency_xml);//Deserialize
       // //Dependency itemToUpdate = dependencies.Find(lk => lk.Id == item.Id);
       // Dependency itemToUpdate = getDependency(dependencyItem)
       //     ?? throw new DalDoesNotExistException($"Dependency with ID= {item.Id} does not exists");
       // //dependencies.Remove(itemToUpdate);
       // xml.RemoveDependency(item.Id);
       // dependencies.Add(item);
       //XMLTools.SaveListToXMLElement(dependencies, s_dependency_xml);
       ////XMLTools.SaveListToXMLSerializer(dependencies, s_dependency_xml);//Serialize
    }
  
}
