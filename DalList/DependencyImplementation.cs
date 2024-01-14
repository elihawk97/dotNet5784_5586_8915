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
        int amountDeleted = DataSource.Dependencies.RemoveAll(item => item.Id == id);
        if (amountDeleted <= 0)
        {
            throw new Exception($"Dependency with ID={id} does Not exist");
        }
    }

    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.Find(item => item.Id == id);

    }

    public List<Dependency> ReadAll()
    {
        List<Dependency> copyList = DataSource.Dependencies.FindAll(item => true);
        if(copyList.Count == 0)
        {
            throw new Exception($"Can not read data since the list is empty");
        }
        return copyList;
    }

    public void Update(Dependency item)
    {
        Dependency toDelete = DataSource.Dependencies.Find(element => element.Id == item.Id);
        if (toDelete == null)
        {
            throw new Exception($"Can't update! No Dependency  with matching ID {item.Id} found");
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
}

