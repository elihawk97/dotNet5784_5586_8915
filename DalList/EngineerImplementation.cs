namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;
using System.Linq;


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
        Engineer copy = item with {Id = id };
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
        Engineer copyChange = copy with { IsActive = false };
        DataSource.Engineers.Remove(copy);
        DataSource.Engineers.Add(copyChange);
    }

    /// <summary>
    /// Retrieves an Engineer object with the specified ID from the DataSource.
    /// </summary>
    /// <param name="id">The ID of the Engineer to be retrieved.</param>
    /// <returns>The Engineer object with the specified ID, or null if not found.</returns>

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

    /// <summary>
    /// Updates an existing Engineer object in the DataSource.
    /// </summary>
    /// <param name="item">The updated Engineer object.</param>
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

    /// <summary>
    /// Resets the DataSource's list of Engineer objects, clearing all entries.
    /// </summary>
    public void Reset()
    {
        DataSource.Engineers.Clear();
    }

    /// <summary>
    /// Retrieves all Engineer objects from the DataSource based on a provided filter.
    /// </summary>
    /// <param name="filter">The filter condition for Engineer objects.</param>
    /// <returns>A sequence of Engineer objects matching the filter condition.</returns>
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Engineers
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Engineers
               select item;
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
}

