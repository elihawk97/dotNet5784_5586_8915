//namespace Dal;
using DO;
using DalApi;
using Dal;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Data.SqlTypes;
using System.Xml;
using System.Data.Common;
using System.Reflection.Metadata;
namespace DalXml;

internal class DependencyImplementation : IDependency
{
    readonly string s_dependencies_xml = "dependencies";

    public int Create(Dependency entity)
    {
        List<Dependency> dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependencies_xml);
        int _id = XMLTools.GetAndIncreaseNextId("data-config", "NextDependencyId");
        Dependency copy = entity with { Id = _id };
        dependencies.Add(copy);
        XMLTools.SaveListToXMLSerializer<Dependency>(dependencies, s_dependencies_xml);
        return _id;
    }

    public void Delete(int id)
    {
        System.Xml.Linq.XElement xElement = XMLTools.LoadListFromXMLElement(s_dependencies_xml);
        // Find the element with the specified ID and remove it
        xElement.Descendants("Dependency") // Replace "YourElementName" with the actual name of your XML element
              .Where(element => (string)element.Attribute("id") == id.ToString())
              .Remove();

        // Save the changes back to the XML file
        XMLTools.SaveListToXMLElement(xElement, s_dependencies_xml);
    }

    public Dependency? Read(int id)
    {
        System.Xml.Linq.XElement xElement = XMLTools.LoadListFromXMLElement(s_dependencies_xml);
        // Find the element with the specified ID and remove it
        XElement foundElement = xElement.Elements("Dependency")
                                        .FirstOrDefault(element => element.Element("Id").ToString() == id.ToString());
        if (foundElement == null)
        {
            // Log the error or handle it accordingly
            Console.WriteLine($"Warning: No Dependency found with ID={id}");
            return null; // Return null or handle the absence of the element
        }

        // Use the ?. operator to avoid null reference exceptions
        int.TryParse(foundElement.Element("Id")?.Value, out int idValue);
        int.TryParse(foundElement.Element("DependentTask")?.Value, out int dependentValue);
        int.TryParse(foundElement.Element("DependentOnTask")?.Value, out int dependentOnValue);

        // Validate that the required values are not null
        if (idValue == 0 || dependentValue == 0 || dependentOnValue == 0)
        {
            // Log the error or handle it accordingly
            Console.WriteLine($"Error: One or more values not found for Dependency with ID={id}");
            return null; // Return null or handle the missing values
        }

        return new Dependency(idValue, dependentValue, dependentOnValue);
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        System.Xml.Linq.XElement xElement = XMLTools.LoadListFromXMLElement(s_dependencies_xml);

        // Convert XML elements to Dependency objects
        List<Dependency> dependencies = xElement.Elements("Dependency")
            .Select(element =>
            {
                int.TryParse(element.Element("Id").Value, out int idValue);
                int.TryParse(element.Element("DependentTask").Value, out int dependentValue);
                int.TryParse(element.Element("DependentOnTask").Value, out int dependentOnValue);

                return new Dependency(idValue, dependentValue, dependentOnValue);
            })
            .ToList();

        // Apply the filter to find the matching Dependency
        Dependency foundDependency = dependencies.FirstOrDefault(filter);

        return foundDependency;
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        System.Xml.Linq.XElement xElement = XMLTools.LoadListFromXMLElement(s_dependencies_xml);
        // Convert XML elements to Dependency objects
        List<Dependency> dependencies = xElement.Elements("Dependency")
            .Select(element =>
            {
                int.TryParse(element.Element("DependentTask")?.Value, out int dependentValue);
                int.TryParse(element.Element("DependentOnTask")?.Value, out int dependentOnValue);
                int.TryParse(element.Element("Id")?.Value, out int id);
                return new Dependency(id, dependentValue, dependentOnValue);
            })
            .ToList();


        if (filter != null)
        {
            dependencies = (List<Dependency>)(from item in dependencies
                           where filter(item)
                           select item);
        }

        if (dependencies.Count() == 0)
        {
            throw new DalDoesNotExistException("Can't Read All! The list is empty");
        }
        return dependencies;
    }

    public void Reset()
    {

        // Load the existing XML file into an XDocument
        XElement xmlDoc = XMLTools.LoadListFromXMLElement(s_dependencies_xml);

        // Remove all child nodes from the root element
        xmlDoc.RemoveNodes();

        // Save the modified XDocument back to the same file, overwriting it
        XMLTools.SaveListToXMLElement(xmlDoc,s_dependencies_xml);
    }

    public void Update(Dependency item)
    {
        System.Xml.Linq.XElement xElement = XMLTools.LoadListFromXMLElement(s_dependencies_xml);
        // Find the element with the specified ID and remove it
        XElement elementToDelete = xElement.Descendants("Dependency") // Replace "YourElementName" with the actual name of your XML element
                                      .FirstOrDefault(element => (string)element.Attribute("id") == item.Id.ToString());
        elementToDelete.Remove();

        XMLTools.SaveListToXMLElement(xElement, s_dependencies_xml);
    }
}

