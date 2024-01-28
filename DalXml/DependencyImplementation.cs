//namespace Dal;
using DO;
using DalApi;
using Dal;
using System.Xml.Linq;

namespace DalXml;

internal class DependencyImplementation : IDependency
{
    readonly string s_dependencies_xml = "dependencies";


    /// <summary>
    /// Creates a new Dependency in the XML file.
    /// </summary>
    /// <param name="entity">The Dependency entity to create.</param>
    /// <returns>The ID of the created Dependency.</returns>
    public int Create(Dependency entity)
    {
        // Load existing XML into an XElement
        XElement xElement = XMLTools.LoadListFromXMLElement(s_dependencies_xml);

        // Get the next ID and increase it
        int _id = XMLTools.GetAndIncreaseNextId("data-config", "NextDependencyId");

        // Create a new XElement for the Dependency
        XElement newElement = new XElement("Dependency",
            new XElement("Id", _id),
            new XElement("DependentTask", entity.DependentTask),
            new XElement("DependentOnTask", entity.DependentOnTask),
            new XElement("IsActive", entity.IsActive)

        ) ;

        // Add the new element to the existing XML
        xElement.Add(newElement);

        // Save the modified XElement back to the same file, overwriting it
        XMLTools.SaveListToXMLElement(xElement, s_dependencies_xml);

        return _id;
   

    }


    /// <summary>
    /// Deletes a Dependency with the specified ID from the XML file.
    /// </summary>
    /// <param name="id">The ID of the Dependency to delete.</param>
    public void Delete(int id)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_dependencies_xml);
        XElement? dep = root.Elements().FirstOrDefault(d => d.Element("Id")!.Value == id.ToString());
        if (dep == null)
        {
            throw new DalDoesNotExistException($"Dependency with ID={id} does not exist in the database");
        }
        dep.Element("IsActive")!.Value = "false";
        XMLTools.SaveListToXMLElement(root, s_dependencies_xml);
    }

    /// <summary>
    /// Reads a Dependency with a specified ID from the XML file.
    /// </summary>
    /// <param name="id">The ID of the Dependency to read.</param>
    /// <returns>The Dependency with the specified ID, or null if not found.</returns>
    public Dependency? Read(int id)
    {
        System.Xml.Linq.XElement xElement = XMLTools.LoadListFromXMLElement(s_dependencies_xml);
        // Find the element with the specified ID and remove it

 
        // Find the first element with the specified ID
        XElement? foundElement = xElement.Elements("Dependency")
                                        .FirstOrDefault(element => element.Element("Id") != null && (int)element.Element("Id") == id && element.Element("IsActive").ToString().Equals("True"));

        if (foundElement == null)
        {
            // Log the error or handle it accordingly
            throw new DalDoesNotExistException($"Dependency with ID={id} does not exist in the database");
        }

        // Use the ?. operator to avoid null reference exceptions
        int.TryParse(foundElement.Element("Id")?.Value, out int idValue);
        int.TryParse(foundElement.Element("DependentTask")?.Value, out int dependentValue);
        int.TryParse(foundElement.Element("DependentOnTask")?.Value, out int dependentOnValue);

        return new Dependency(idValue, dependentValue, dependentOnValue);
    }


    /// <summary>
    /// Reads a Dependency based on a filter condition from the XML file.
    /// </summary>
    /// <param name="filter">The filter condition.</param>
    /// <returns>The matching Dependency, or null if not found.</returns>

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
        if(foundDependency == null) {
            throw new DalDoesNotExistException($"No Dependency fitting that filter exists in the database");
        }
        return foundDependency;
    }

    /// <summary>
    /// Reads all Dependencies from the XML file.
    /// </summary>
    /// <param name="filter">Optional filter condition.</param>
    /// <returns>A list of Dependencies matching the filter.</returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        System.Xml.Linq.XElement xElement = XMLTools.LoadListFromXMLElement(s_dependencies_xml);
        // Convert XML elements to Dependency objects
        List<Dependency> dependencies = xElement.Elements("Dependency")
            .Where(element => (bool?)element.Element("IsActive") ?? false) // Check IsActive attribute
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

    /// <summary>
    /// Resets the XML file by removing all Dependency elements.
    /// </summary>
    public void Reset()
    {

        // Load the existing XML file into an XDocument
        XElement xmlDoc = XMLTools.LoadListFromXMLElement(s_dependencies_xml);

        // Remove all child nodes from the root element
        xmlDoc.RemoveNodes();
        // Save the modified XDocument back to the same file, overwriting it
        XMLTools.SaveListToXMLElement(xmlDoc,s_dependencies_xml);
    }

    /// <summary>
    /// Updates a Dependency in the XML file.
    /// </summary>
    /// <param name="item">The Dependency object with updated values.</param>
    public void Update(Dependency item)
    {
        int id = item.Id;
        XElement root = XMLTools.LoadListFromXMLElement(s_dependencies_xml);
        XElement? dep = root.Elements().FirstOrDefault(d => d.Element("Id")!.Value == id.ToString());
        if (dep == null)
        {
            throw new DalDoesNotExistException($"Dependency with ID={item.Id} does not exist in the database");
        }
        dep.Element("DependentTask")!.Value = item.DependentTask.ToString();
        dep.Element("DependentOnTask")!.Value = item.DependentOnTask.ToString();
        XMLTools.SaveListToXMLElement(root, s_dependencies_xml);
    }

    }



