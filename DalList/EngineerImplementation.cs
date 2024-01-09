namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
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
        int amountDeleted = DataSource.Engineers.RemoveAll(e => e.Id == id);
        if (amountDeleted <= 0)
        {
            throw new Exception($"Can't Delete! Dependency with ID={id} does Not exist");
        }
    }

    public Engineer? Read(int id)
    {
        return DataSource.Engineers.Find(e => e.Id == id);
/*        if (copy == null)
        {
            throw new Exception($"Can not read Engineer. Engineer with ID={id} does Not exist");
        }
        return copy;*/

    }

    public List<Engineer> ReadAll()
    {
        List<Engineer> copyList = DataSource.Engineers.FindAll(e => true);
        if (copyList.Count == 0)
        {
            throw new Exception($"Can not read data since the Engineer list is empty");
        }
        return copyList;
    }

    public void Update(Engineer item)
    {
        Engineer existingItem = DataSource.Engineers.Find(e => e.Id == item.Id);

        if (existingItem == null)
        {
            throw new Exception($"Can't update! No Engineer with matching ID {item.Id} found");
        }

        // Remove the old object from the list
        DataSource.Engineers.Remove(existingItem);

        // Add the updated object to the list
        DataSource.Engineers.Add(item);
    }
}

