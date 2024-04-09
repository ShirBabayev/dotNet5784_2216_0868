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
            Id = (int)(d.Element("Id") ?? throw new FormatException("can't convert id")),
            DependentTaskId = d.ToIntNullable("DependentTaskId") ?? throw new FormatException("can't convert dependent task id"),
            DependentOnTaskId = d.ToIntNullable("DependentOnTaskId") ?? throw new FormatException("can't convert dependent on task id")
        };
    }
    public int Create(Dependency item)
    {
        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependency_xml);
        int newId = XMLTools.GetAndIncreaseNextId(s_data_config_xml, "nextDependencyId");//next id number config func
        Dependency newItem = item with { Id = newId };
        XElement item1 = create(newItem);
        XElement create(Dependency newItem) => new XElement("dependency",
            new XElement("Id", newItem.Id),
            new XElement("DependentTaskId", newItem.DependentTaskId),
            new XElement("DependentOnTaskId", newItem.DependentOnTaskId)
            );
        dependencies.Add(item1);
        XMLTools.SaveListToXMLElement(dependencies, s_dependency_xml);
        return item.Id;
    }

    public void Delete(int id)
    {
        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependency_xml);
        XElement dependencyItem = dependencies.Elements().FirstOrDefault(dependency => (int?)dependency.Element("Id") == id)!
            ?? throw new DalDoesNotExistException($"Dependency with ID={id} does not exists");
        //souldn't i do something like dependencies.remove(dependencyItem)   ??
        dependencyItem.Remove();
        XMLTools.SaveListToXMLElement(dependencies, s_dependency_xml);
    }

    public Dependency? Read(int id)
    {
        XElement dependencyItem = XMLTools.LoadListFromXMLElement(s_dependency_xml).Elements().FirstOrDefault(dependency => (int?)dependency.Element("Id") == id)!;
        return dependencyItem is null ? null : getDependency(dependencyItem);

    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return XMLTools.LoadListFromXMLElement(s_dependency_xml).Elements().Select(Dependency => getDependency(Dependency)).FirstOrDefault(filter);
    }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null)
    {
        return filter == null ? XMLTools.LoadListFromXMLElement(s_dependency_xml).Elements().Select(Dependency => getDependency(Dependency))
                              : XMLTools.LoadListFromXMLElement(s_dependency_xml).Elements().Select(Dependency => getDependency(Dependency)).Where(filter);
    }
    public void Update(Dependency item)
    {
        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependency_xml);
        XElement dependencyItem = dependencies.Elements().FirstOrDefault(dependency => (int?)dependency.Element("Id") == item.Id)!;
        if (dependencyItem != null)
        {
            dependencyItem.Remove();
            dependencies.Add(item);
        }
        else
            throw new DalDoesNotExistException($"Dependency with ID= {item.Id} does not exists");
        XMLTools.SaveListToXMLElement(dependencies, s_dependency_xml);
    }
    public void Reset()
    {
        List<DO.Dependency> emptyList = new List<DO.Dependency>();
        XMLTools.SaveListToXMLSerializer(emptyList, s_dependency_xml);
        //XElement root = new XElement("ArrayOfDependencies");
        //XMLTools.SaveListToXMLElement(root, s_dependency_xml);
    }
}
