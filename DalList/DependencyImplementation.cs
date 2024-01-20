namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int id = DataSource.Config.GetNextDependencyId();
        Dependency copy = item with { Id = id };
        DataSource.Dependencies.Add(copy);
        return id;
    }

    public void Delete(int id)
    {
        Dependency copy = DataSource.Dependencies.FirstOrDefault(e => e.Id == id);
        if(copy == null)
        {
            throw new DalDoesNotExistException($"Dependency with ID={id} does not exist, so it can not be deleted");
        }
        Dependency copyChange = copy with { IsActive = false };
        DataSource.Dependencies.Remove(copy);
        DataSource.Dependencies.Add(copyChange);
    }

    public Dependency? Read(int id)
    {
        Dependency toRead = DataSource.Dependencies.FirstOrDefault(item => item.Id == id);
        if(toRead == null)
        {
            throw new DalDoesNotExistException($"Can't Read! Dependency with ID={id} does not exist");
        }
        return toRead;
    }

    public Dependency Read(Func<Dependency, bool>? filter = null) //stage 2
    {
        Dependency toRead;
        if (filter != null)
        {
            toRead = DataSource.Dependencies.FirstOrDefault(filter);
        }
        else
        {
            toRead = DataSource.Dependencies.FirstOrDefault();
        }

        if (toRead == null)
        {
            throw new DalDoesNotExistException("No task with fits this filter argument");
        }

        return toRead;
    }


    public void Update(Dependency item)
    {

        Dependency toDelete = DataSource.Dependencies.FirstOrDefault(element => element.Id == item.Id);
        if (toDelete == null)
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
        DataSource.Dependencies.Clear(); 
    }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null) //stage 2
    {
        IEnumerable<Dependency> list;
        if (filter != null)
        {
            list = from item in DataSource.Dependencies
                   where filter(item)
                   select item;
        }
        else
        {
            list = from item in DataSource.Dependencies
                   select item;
        }
        if(list == null)
        {
            throw new DalDoesNotExistException("Can't Read All! The list is empty");
        }
        return list;
    }

}

