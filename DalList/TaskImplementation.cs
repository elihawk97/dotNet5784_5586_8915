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
           throw new DalDoesNotExistException($"Can't read Task with ID={id}, it does not exist");
        }

        return task;
    }

    public Task Read(Func<Task, bool>? filter = null) //stage 2
    {
        Task toRead;
        if (filter != null)
        {
            toRead = DataSource.Tasks.FirstOrDefault(filter);
        }
        else
        {
            toRead = DataSource.Tasks.FirstOrDefault();
        }

        if(toRead == null)
        {
            throw new DalDoesNotExistException("No task with fits this filter argument");
        }

        return toRead;
    }

    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null) //stage 2
    {
        IEnumerable<Task> tasks;
        if (filter == null)
            tasks = DataSource.Tasks.Select(item => item);
        else
            tasks = DataSource.Tasks.Where(filter);
        if(tasks == null)
        {
            throw new DalDoesNotExistException("Can not read all tasks, the Task list is empty");
        }

        return tasks;
    }

    public void Update(Task item) 
    {
        Task existingItem = DataSource.Tasks.FirstOrDefault(e => e.Id == item.Id);

        if (existingItem == null)
        {
            throw new DalDoesNotExistException($"Task with ID={item.Id} does not exist");
        }

        // Replace the old object in the list with the updated object
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


