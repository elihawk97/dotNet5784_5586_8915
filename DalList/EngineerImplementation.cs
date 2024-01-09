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
        int itemsDeleted = DataSource.Engineers.RemoveAll(e => e.Id == id);

        //Throw Exception using number of ItemsDeleted
    }

    public Engineer? Read(int id)
    {
        return DataSource.Engineers.Find(e => e.Id == id);
    }

    public List<Engineer> ReadAll()
    {
        return DataSource.Engineers.FindAll(e => true);
    }

    public void Update(Engineer item)
    {
        Engineer existingItem = DataSource.Engineers.Find(e => e.Id == item.Id);

        if (existingItem == null)
        {
            // Object with the specified Id does not exist, throw an exception
        }

        // Remove the old object from the list
        DataSource.Engineers.Remove(existingItem);

        // Add the updated object to the list
        DataSource.Engineers.Add(item);
    }
}

