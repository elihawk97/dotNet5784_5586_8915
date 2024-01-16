namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

internal class TaskImplementation : ITask
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
        Task copy = DataSource.Tasks.Find(e => e.Id == id) with { IsActive = false };
        Task copyChange = copy with { IsActive = false };
        DataSource.Tasks.Remove(copy);
        DataSource.Tasks.Add(copyChange);
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

    public void Reset()
    {
        DataSource.Tasks.Clear();
    }
}
