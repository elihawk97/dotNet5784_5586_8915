namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;
using System.Linq;

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
        Task? copy = DataSource.Tasks.FirstOrDefault(e => e.Id == id);

        if (copy == null)
        {
            throw new DalDoesNotExistException($"Task with ID={id} does not exist");
        }

        Task copyChange = copy with { IsActive = false };
        DataSource.Tasks.Remove(copy);
        DataSource.Tasks.Add(copyChange);
    }

    public Task? Read(int id)
    {
        Task? task = DataSource.Tasks.FirstOrDefault(e => e.Id == id);

        if (task == null)
        {
            throw new DalDoesNotExistException($"Task with ID={id} does not exist");
        }

        return task;
    }
    
    public List<Task> ReadAll()
    {
        List<Task> activeTasks = DataSource.Tasks
                                      .Where(task => task.IsActive)
                                      .ToList();

        if (activeTasks.Count == 0)
        {
            throw new DalDoesNotExistException("There are no tasks to read.");
        }

        return activeTasks;
    }

    public void Update(Task item) 
    {
        Task existingItem = DataSource.Tasks.FirstOrDefault(e => e.Id == item.Id);

        if (existingItem == null)
        {
            throw new DalDoesNotExistException($"Task with ID={item.Id} does not exist");
        }

        // Replace the old object in the list with the updated object
        int index = DataSource.Tasks.IndexOf(existingItem);
        DataSource.Tasks[index] = item;

    }

    public void Reset()
    {
        DataSource.Tasks.Clear();
    }
}
