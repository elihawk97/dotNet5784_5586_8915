namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

/// <summary>
/// Implementation of the IEngineer interface for CRUD operations on Engineer objects.
/// </summary>
internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// Creates a new Engineer object and adds it to the DataSource.
    /// </summary>
    /// <param name="item">The Engineer object to be created.</param>
    /// <returns>The identifier (ID) of the newly created Engineer.</returns>

    public int Create(Engineer item)
    {
        int id = DataSource.Config.GetNextEngineerId();
        Engineer copy = item with { Id = id };
        DataSource.Engineers.Add(copy);
        return id;
    }

    /// <summary>
    /// Marks an Engineer as inactive by setting its IsActive property to false.
    /// </summary>
    /// <param name="id">The ID of the Engineer to be marked as inactive.</param>

    public void Delete(int id)
    {
        Engineer copy = DataSource.Engineers.FirstOrDefault(item => item.Id == id);
        if (copy == null)
        {
            throw new DalDoesNotExistException($"Can not delete Engineer. Engineer with ID={id} does Not exist");
        }
        DataSource.Engineers.Remove(copy);
    }

    /// <summary>
    /// Retrieves an Engineer object with the specified ID from the DataSource.
    /// </summary>
    /// <param name="id">The ID of the Engineer to be retrieved.</param>
    /// <returns>The Engineer object with the specified ID, or null if not found.</returns>
    public Engineer? Read(int id)
    {
        Engineer copy = DataSource.Engineers.FirstOrDefault(item => item.Id == id);
        if (copy == null)
        {
            throw new DalDoesNotExistException($"Can not read Engineer. Engineer with ID={id} does Not exist");
        }
        return copy;

    }

    /// <summary>
    /// Retrieves an Engineer object from the DataSource based on a provided filter.
    /// </summary>
    /// <param name="filter">The filter condition for the Engineer object.</param>
    /// <returns>The Engineer object matching the filter condition, or null if not found.</returns>

    public Engineer Read(Func<Engineer, bool>? filter = null) //stage 2
    {
        Engineer toRead;
        if (filter != null)
        {
            toRead = DataSource.Engineers.FirstOrDefault(filter);
        }
        else
        {
            toRead = DataSource.Engineers.FirstOrDefault();
        }

        if (toRead == null)
        {
            throw new DalDoesNotExistException("No engineer fits this filter argument");
        }

        return toRead;
    }


    /// <summary>
    /// Updates an engineer
    /// The id of the engineer passed should match one of those in the engineers list
    /// </summary>
    /// <param name="engineer">Engineer to update</param>
    /// <exception cref="DalDoesNotExistException">Throw if the engineer is not in the list</exception>
    public void Update(Engineer engineer)
    {
        Engineer existingItem = DataSource.Engineers.FirstOrDefault(item => item.Id == engineer.Id);

        if (existingItem == null)
        {
            throw new DalDoesNotExistException($"Can not update Engineer. Engineer with ID={engineer.Id} does Not exist");
        }

        // Remove the old object from the list
        DataSource.Engineers.Remove(existingItem);

        // Add the updated object to the list
        DataSource.Engineers.Add(engineer);
    }

    /// <summary>
    /// Resets the DataSource's list of Engineer objects, clearing all entries.
    /// </summary>
    public void Reset()
    {
        DataSource.Engineers = new List<Engineer>();
    }


    /// <summary>
    /// Retrieves all Engineer objects from the DataSource based on a provided filter.
    /// </summary>
    /// <param name="filter">The filter condition for Engineer objects.</param>
    /// <returns>A sequence of Engineer objects matching the filter condition.</returns>
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    {
        IEnumerable<Engineer> engineers;
        if (filter != null)
        {
            engineers = from item in DataSource.Engineers
                        where filter(item)
                        select item;
        }
        else
        {
            engineers = from item in DataSource.Engineers
                        select item;
        }

        if (engineers.Count() == 0)
        {
            throw new DalDoesNotExistException($"Can not read all Engineers. The Engineer list is empty");
        }

        return engineers;

    }
}

