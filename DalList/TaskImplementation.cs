namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

/// <summary>
/// Implementation of the ITask interface for CRUD operations on Task objects.
/// </summary>
internal class TaskImplementation : ITask
{

    /// <summary>
    /// Creates a new Task object and adds it to the DataSource.
    /// </summary>
    /// <param name="item">The Task object to be created.</param>
    /// <returns>The identifier (ID) of the newly created Task.</returns>
    public int Create(Task item)
    {

        // Check if start date is not before project start date
        if (item.ProjectedStartDate < DataSource.Config.getStartDate())
        {
            throw new InvalidTime("Task start date cannot be before project start date");
        }

        // Check if end date is not after project end date
        if (item.DeadLine > DataSource.Config.getEndDate())
        {
            throw new InvalidTime("Task end date cannot be after project end date");
        }

        int id = DataSource.Config.GetNextTaskId();
        Task copy = item with { Id = id };
        
        DataSource.Tasks.Add(copy);
        return id;
    }

    /// <summary>
    /// Marks a Task as inactive by setting its IsActive property to false.
    /// </summary>
    /// <param name="id">The ID of the Task to be marked as inactive.</param>
    public void Delete(int id)
    {
        Task? copy = DataSource.Tasks.FirstOrDefault(e => e.Id == id);

        if (copy == null)
        {
            throw new DalDoesNotExistException($"Task with ID={id} does not exist");
        }

        DataSource.Tasks.Remove(copy);
    }

    /// <summary>
    /// Retrieves a Task object with the specified ID from the DataSource.
    /// </summary>
    /// <param name="id">The ID of the Task to be retrieved.</param>
    /// <returns>The Task object with the specified ID, or null if not found.</returns>
    public Task? Read(int id)
    {
        Task? task = DataSource.Tasks.FirstOrDefault(e => e.Id == id);

        if (task == null)
        {
           throw new DalDoesNotExistException($"Can't read Task with ID={id}, it does not exist");
        }

        return task;
    }

    /// <summary>
    /// Retrieves a single Task object from the DataSource based on a provided filter.
    /// </summary>
    /// <param name="filter">The filter condition for the Task object.</param>
    /// <returns>The Task object matching the filter condition.</returns>
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

    /// <summary>
    /// Retrieves all Task objects from the DataSource based on a provided filter.
    /// </summary>
    /// <param name="filter">The filter condition for Task objects.</param>
    /// <returns>A collection of Task objects matching the filter condition.</returns>
    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null) //stage 2
    {
        IEnumerable<Task> tasks;
        if (filter == null)
            tasks = DataSource.Tasks.Select(item => item);
        else
            tasks = DataSource.Tasks.Where(filter);
        if(tasks.Count() == 0)
        {
            throw new DalDoesNotExistException("Can not read all tasks, the Task list is empty");
        }

        return tasks;
    }

    /// <summary>
    /// Updates an existing Task object in the DataSource.
    /// </summary>
    /// <param name="item">The updated Task object.</param>
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

    /// <summary>
    /// Resets the DataSource's list of Task objects, clearing all entries.
    /// </summary>
    public void Reset()
    {
        DataSource.Tasks = new List<Task>();
    }
}


