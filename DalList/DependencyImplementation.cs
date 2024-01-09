namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
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
        int amountDeleted = DataSource.Dependencies.RemoveAll(item => item.id == id);
        if (amountDeleted < 0)
        {
            throw new Exception($"Dependency with ID={id} does Not exist");
        }
    }

    public Dependency? Read(int id)
    {
        Dependency copy = DataSource.Dependencies.Find(item => item.id == id);
        if(copy == null)
        {
            throw new Exception($"Can not read dependency. Dependency with ID={id} does Not exist");
        }
        return copy;
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
        bool isDeleted = DataSource.Dependencies.Remove(element => element.id == item.id);
        if (!isDeleted)
        {
            throw new Exception("Can't update no item with matching ID found");
        }
        DataSource.Dependencies.Add(item);           
    }
}
