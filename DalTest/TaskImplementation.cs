namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int id = DataSource.Config.GetNextTaskId();
        Task copy = item with { Id = id };
        DataSource.Tasks.Add(copy);
        return id;
    }

    public void Delete(int id)
    {
        int itemsDeleted = DataSource.Engineers.RemoveAll(e => e.Id == id);

        //Throw Exception using number of ItemsDeleted
    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(e => e.Id == id);
    }

    public List<Task> ReadAll()
    {
        return DataSource.Tasks.FindAll(e => true);
    }

    public void Update(Task item) 
    {
        Task existingItem = DataSource.Tasks.Find(e => e.Id == item.Id);

        if (existingItem == null)
        {
            // Object with the specified Id does not exist, throw an exception
        }

        // Remove the old object from the list
        DataSource.Tasks.Remove(existingItem);

        // Add the updated object to the list
        DataSource.Tasks.Add(item);
 
    }
}
