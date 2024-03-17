
using DO;
using DalApi;
using Dal;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    readonly string s_engineers_xml = "engineers";

    public int Create(Engineer entity)
    {
            List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

            int nextId = XMLTools.GetAndIncreaseNextId("data-config", "NextEngineerId", false);
            entity.Id = nextId;
            engineers.Add(entity);

            XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
            return nextId;
    }

    public void Delete(int id)
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

    public Engineer? Read(int id)
    {
            List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

            Engineer? engineerToRead = engineers.FirstOrDefault(e => e.Id == id);

            if (engineerToRead == null || engineerToRead.IsActive == false)
            {
                throw new DalDoesNotExistException($"Engineer with ID {id} does not exist.");
            }

            return engineerToRead;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
            List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
            Engineer? engineerToRead = engineers.FirstOrDefault(filter);

            if (engineerToRead == null)
            {
                throw new DalDoesNotExistException($"Engineer does not exist.");
            }

            return engineerToRead;
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
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

    public void Reset()
    {
        List<Engineer> engineers = new List<Engineer>();
        XMLTools.GetAndIncreaseNextId("data-config", "NextEngineerId", true);
        XMLTools.SaveListToXMLSerializer(engineers, s_engineers_xml);
    }

    public void Update(Engineer item)
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
}
