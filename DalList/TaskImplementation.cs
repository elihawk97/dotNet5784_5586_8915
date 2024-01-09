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
        int amountDeleted = DataSource.Engineers.RemoveAll(e => e.Id == id);
        if (amountDeleted <= 0)
        {
            throw new Exception($"Can't Delete! Task with ID={id} does Not exist");
        }
    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(e => e.Id == id);
/*        if (copy == null)
        {
            throw new Exception($"Can not read Engineer. Engineer with ID={id} does Not exist");
        }
        return copy;*/
    }

    public List<Task> ReadAll()
    {
        List<Task> copyList = DataSource.Tasks.FindAll(e => true);
        if (copyList.Count == 0)
        {
            throw new Exception($"Can not read data since the Task list is empty");
        }
        return copyList;
    }

    public void Update(Task item) 
    {
        Task existingItem = DataSource.Tasks.Find(e => e.Id == item.Id);

        if (existingItem == null)
        {
            throw new Exception($"Can't update! No Task with matching ID {item.Id} found");
        }

        // Remove the old object from the list
        DataSource.Tasks.Remove(existingItem);

        // Add the updated object to the list
        DataSource.Tasks.Add(item);
 
    }
}
