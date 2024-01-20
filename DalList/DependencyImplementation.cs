namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;


/// <summary>
/// Implementation of the IDependency interface for CRUD operations on Dependency objects.
/// </summary>

internal class DependencyImplementation : IDependency
{

    /// <summary>
    /// Creates a new Dependency object and adds it to the DataSource.
    /// </summary>
    /// <param name="item">The Dependency object to be created.</param>
    /// <returns>The identifier (ID) of the newly created Dependency.</returns>
    public int Create(Dependency item)
    {
        int id = DataSource.Config.GetNextDependencyId();
        Dependency copy = item with { Id = id };
        DataSource.Dependencies.Add(copy);
        return id;
    }


    /// <summary>
    /// Marks a Dependency as inactive by setting its IsActive property to false.
    /// </summary>
    /// <param name="id">The ID of the Dependency to be marked as inactive.</param>
    public void Delete(int id)
    {
        Dependency copy = DataSource.Dependencies.FirstOrDefault(e => e.Id == id);
        Dependency copyChange = copy with { IsActive = false };
        DataSource.Dependencies.Remove(copy);
        DataSource.Dependencies.Add(copyChange);
    }

    /// <summary>
    /// Retrieves a Dependency object with the specified ID from the DataSource.
    /// </summary>
    /// <param name="id">The ID of the Dependency to be retrieved.</param>
    /// <returns>The Dependency object with the specified ID, or null if not found.</returns>
    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.FirstOrDefault(item => item.Id == id);

    }

    /// <summary>
    /// Retrieves all active Dependency objects from the DataSource.
    /// </summary>
    /// <returns>A list of active Dependency objects.</returns>
    public List<Dependency> ReadAll()
    {
        List<Dependency> activeDependency = DataSource.Dependencies.Where(Dependency => Dependency.IsActive == true).ToList();

        if(activeDependency.Count == 0)
        {
            throw new Exception($"Can not read data since the list is empty");
        }
        return activeDependency;
    }

    /// <summary>
    /// Updates an existing Dependency object in the DataSource.
    /// </summary>
    /// <param name="item">The updated Dependency object.</param>
    public void Update(Dependency item)
    {

        Dependency toDelete = DataSource.Dependencies.FirstOrDefault(element => element.Id == item.Id);
        if (toDelete == null)
        {
            throw new Exception($"Can't update! No Dependency  with matching ID {item.Id} found");
        }
        // Remove the old object from the list
        DataSource.Dependencies.Remove(toDelete);

        // Add the updated object to the list
        DataSource.Dependencies.Add(item);

    }

    /// <summary>
    /// Resets the DataSource's list of Dependency objects, clearing all entries.
    /// </summary>
    public void Reset()
    {
        DataSource.Dependencies.Clear(); 
    }

    /// <summary>
    /// Retrieves all Dependency objects from the DataSource based on a provided filter.
    /// </summary>
    /// <param name="filter">The filter condition for Dependency objects.</param>
    /// <returns>A sequence of Dependency objects matching the filter condition.</returns>
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Dependencies
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencies
               select item;
    }

    /// <summary>
    /// Retrieves a Dependency object from the DataSource based on a provided filter.
    /// </summary>
    /// <param name="filter">The filter condition for the Dependency object.</param>
    /// <returns>The Dependency object matching the filter condition, or null if not found.</returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        throw new NotImplementedException();
    }
}

