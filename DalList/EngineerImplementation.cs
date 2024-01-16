namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;
using System.Linq;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        int id = DataSource.Config.GetNextEngineerId();
        Engineer copy = item with {Id = id };
        DataSource.Engineers.Add(copy);
        return id; 
    }

    public void Delete(int id)
    {
        Engineer copy = DataSource.Engineers.FirstOrDefault(item => item.Id == id);
        Engineer copyChange = copy with { IsActive = false };
        DataSource.Engineers.Remove(copy);
        DataSource.Engineers.Add(copyChange);
    }

    public Engineer? Read(int id)
    {
        Engineer copy = DataSource.Engineers.FirstOrDefault(item => item.Id == id);
        return copy;
/*        if (copy == null)
        {
            throw new Exception($"Can not read Engineer. Engineer with ID={id} does Not exist");
        }
        return copy;*/

    }

    public List<Engineer> ReadAll()
    {
        List<Engineer> activeEngineers= DataSource.Engineers.Where(Engineer => Engineer.IsActive == true).ToList();
        if (activeEngineers.Count == 0)
        {
            throw new Exception($"Can not read data since the Engineer list is empty");
        }
        return activeEngineers;
    }

    public void Update(Engineer item)
    {
        Engineer existingItem = DataSource.Engineers.FirstOrDefault(item => item.Id == item.Id);

        if (existingItem == null)
        {
            throw new Exception($"Can't update! No Engineer with matching ID {item.Id} found");
        }

        // Remove the old object from the list
        DataSource.Engineers.Remove(existingItem);

        // Add the updated object to the list
        DataSource.Engineers.Add(item);
    }

    public void Reset()
    {
        DataSource.Engineers.Clear();
    }
}

