namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;
using System.Threading.Tasks;


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
        if(copy == null)
        {
            throw new DalDoesNotExistException($"Dependency with ID={id} does not exist, so it can not be deleted");
        }
        DataSource.Dependencies.Remove(copy);
    }

    /// <summary>
    /// Retrieves a Dependency object with the specified ID from the DataSource.
    /// </summary>
    /// <param name="id">The ID of the Dependency to be retrieved.</param>
    /// <returns>The Dependency object with the specified ID, or null if not found.</returns>
    public Dependency? Read(int id)
    {
        Dependency? toRead = DataSource.Dependencies.FirstOrDefault(item => item.Id == id);
        if(toRead == null)
        {
            throw new DalDoesNotExistException($"Can't Read! Dependency with ID={id} does not exist");
        }
        return toRead;
    }

    /// <summary>
    /// Retrieves all active Dependency objects from the DataSource.
    /// </summary>
    /// <returns>A list of active Dependency objects.</returns>
    public List<Dependency> ReadAll()
    {
        Dependency? toRead;
        if (filter != null)
        {
            toRead = DataSource.Dependencies.FirstOrDefault(filter);
        }
        else
        {
            toRead = DataSource.Dependencies.FirstOrDefault();
        }

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

        Dependency? toDelete = DataSource.Dependencies.FirstOrDefault(element => element.Id == item.Id);
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
        DataSource.Dependencies = new List<Dependency>(); 
    }

    /// <summary>
    /// Retrieves all Dependency objects from the DataSource based on a provided filter.
    /// </summary>
    /// <param name="filter">The filter condition for Dependency objects.</param>
    /// <returns>A sequence of Dependency objects matching the filter condition.</returns>
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null) //stage 2
    {
        IEnumerable<Dependency> dependencies;
        if (filter != null)
        {
            dependencies = from item in DataSource.Dependencies
                   where filter(item)
                   select item;
        }
        else
        {
            dependencies = from item in DataSource.Dependencies
                   select item;
        }
        if(dependencies.Count() == 0)
        {
            throw new DalDoesNotExistException("Can't Read All! The list is empty");
        }
        return dependencies;
    }
}

