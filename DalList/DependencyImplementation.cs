namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading;

/// <summary>
/// Implementation for the Dependency interface
/// </summary>
internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// Create a Dependency record by adding the object to the dependency list
    /// </summary>
    /// <param name="item">Depedency</param>
    /// <returns></returns>
    public int Create(Dependency item)
    {
        int id = DataSource.Config.NextDependencyId;
        Dependency copy = item with { Id = id };
        if (DataSource.Dependencies.Any(d => (d.DependentTask == copy.DependentTask && d.DependentOnTask == copy.DependentOnTask))){
        }
        else {
            DataSource.Dependencies.Add(copy); 
        }
        return id;
    }

    public void Activate(int id)
    {
        Dependency? copy = DataSource.Dependencies.FirstOrDefault(e => e.Id == id);
        if (copy == null)
        {
            throw new DalDoesNotExistException($"Dependency with ID={id} does not exist in deleted state, so it can not be deleted");
        }
        copy.IsActive = true;
    }

    public void Delete(int id)
    {
        Dependency? copy = DataSource.Dependencies.FirstOrDefault(e => e.Id == id);
        if (copy == null)
        {
            throw new DalDoesNotExistException($"Dependency with ID={id} does not exist, so it can not be deleted");
        }
        copy.IsActive = false;

    }

    public Dependency? Read(int id)
    {
        Dependency? toRead = DataSource.Dependencies.FirstOrDefault(item => item.Id == id);
        if (toRead == null || toRead.IsActive == false)
        {
            throw new DalDoesNotExistException($"Can't Read! Dependency with ID={id} does not exist");
        }
        return toRead;
    }

    public Dependency Read(Func<Dependency, bool>? filter = null) //stage 2
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

        if (toRead == null || toRead.IsActive == false)
        {
            throw new DalDoesNotExistException("No task with fits this filter argument");
        }

        return toRead;
    }


    public void Update(Dependency item)
    {

        Dependency? toDelete = DataSource.Dependencies.FirstOrDefault(element => element.Id == item.Id);
        if (toDelete == null || toDelete.IsActive == false)
        {
            throw new DalDoesNotExistException($"Can't update! No Dependency  with matching ID {item.Id} found");
        }
        // Remove the old object from the list
        DataSource.Dependencies.Remove(toDelete);

        // Add the updated object to the list
        DataSource.Dependencies.Add(item);

    }

    public void Reset()
    {
        DataSource.Dependencies = new List<Dependency>();
    }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter) //stage 2
    {
        IEnumerable<Dependency> dependencies;
        if (filter != null)
        {
            dependencies = from item in DataSource.Dependencies
                           where filter(item) && item.IsActive == true
                           select item;
        }
        else
        {
            dependencies = from item in DataSource.Dependencies
                           where item.IsActive == true
                           select item;
        }
        if (dependencies.Count() == 0)
        {
            //throw new DalDoesNotExistException("Can't Read All! The list is empty");
        }
        return dependencies;
    }

}

