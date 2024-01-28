
using DO;
using DalApi;
using Dal;

namespace DalXml;

internal class EngineerImplementation : IEngineer
{
    readonly string s_engineers_xml = "engineers";

    public int Create(Engineer entity)
    {

        try
        {
            List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

            int nextId = XMLTools.GetAndIncreaseNextId("data-config", "NextEngineerId");
            entity.Id = nextId;
            engineers.Add(entity);

            XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
            return nextId;

        }

        catch (DalXMLFileLoadCreateException ex)
        {
            Console.WriteLine($"Error creating Engineer: {ex.Message}");
            return -1; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error creating task: {ex.Message}");
            return -1; 
        }

    }

    public void Delete(int id)
    {

        try
        {
            List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

            Engineer? engineerToDelete = engineers.FirstOrDefault(e => e.Id == id);

            if (engineerToDelete == null)
            {
                throw new DalDoesNotExistException($"Engineer with ID {id} does not exist.");
            }

            engineerToDelete.IsActive = false;

            XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
        }

        catch (DalXMLFileLoadCreateException ex)
        {
            Console.WriteLine($"Error creating Engineer: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error creating task: {ex.Message}");
        }

    }

    public Engineer? Read(int id)
    {
        try
        {
            List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

            Engineer? engineerToRead = engineers.FirstOrDefault(e => e.Id == id);

            if (engineerToRead == null || engineerToRead.IsActive == false)
            {
                throw new DalDoesNotExistException($"Engineer with ID {id} does not exist.");
            }

            return engineerToRead;
        }
        catch (DalXMLFileLoadCreateException ex)
        {
            Console.WriteLine($"Error creating Engineer: {ex.Message}");
            return null; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error creating task: {ex.Message}");
            return null; 
        }
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        try
        {
            List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
            Engineer? engineerToRead = engineers.FirstOrDefault(filter);

            if (engineerToRead == null)
            {
                throw new DalDoesNotExistException($"Engineer does not exist.");
            }

            return engineerToRead;
        }
        catch (DalXMLFileLoadCreateException ex)
        {
            Console.WriteLine($"Error creating Engineer: {ex.Message}");
            return null; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error creating task: {ex.Message}");
            return null; 
        }
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        try
        {
            List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

            if (engineers.Count == 0)
            {
                throw new DalDoesNotExistException($"XML File is empty");
            }

            // Add a default filter for isActive = true
            Func<Engineer, bool> isActiveFilter = engineer => engineer.IsActive;

            // Apply user-provided filter if any

            if (filter != null)
            {
                isActiveFilter = engineer => filter(engineer) && engineer.IsActive;
            }

            return engineers.Where(isActiveFilter);
        }

        catch (DalXMLFileLoadCreateException ex)
        {
            Console.WriteLine($"Error creating Engineer: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error creating task: {ex.Message}");
            return null;
        }
    }

    public void Reset()
    {
        try { 
        List<Engineer> engineers = new List<Engineer>();
        XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
        }
        catch (DalXMLFileLoadCreateException ex)
        {
            Console.WriteLine($"Error creating Engineer: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error creating task: {ex.Message}");
        }
    }

    public void Update(Engineer item)
    {
        try
        {
            List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

            Engineer? existingItem = engineers.FirstOrDefault(e => e.Id == item.Id);


            if (existingItem == null)
            {
                throw new DalDoesNotExistException($"Can not update Engineer. Engineer with ID={item.Id} does not exist");
            }

            engineers.Remove(existingItem); 
            engineers.Add(item);

            XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);

        }
        catch (DalXMLFileLoadCreateException ex)
        {
            Console.WriteLine($"Error creating Engineer: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error creating task: {ex.Message}");
        }
    }
}
