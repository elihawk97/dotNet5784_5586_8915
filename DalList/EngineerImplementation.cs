namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;
using System.Linq;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        int id = DataSource.Config.GetNextEngineerId();
        Engineer copy = item with {Id = id };
        DataSource.Engineers.Add(copy);
        return id; 
    }

    public void Delete(int id)
    {
        Engineer copy = DataSource.Engineers.FirstOrDefault(item => item.Id == id);
        if (copy == null)
        {
            throw new DalDoesNotExistException($"Can not delete Engineer. Engineer with ID={id} does Not exist");
        }
        Engineer copyChange = copy with { IsActive = false };
        DataSource.Engineers.Remove(copy);
        DataSource.Engineers.Add(copyChange);
    }

    public Engineer? Read(int id)
    {
        Engineer copy = DataSource.Engineers.FirstOrDefault(item => item.Id == id);
        if (copy == null)
        {
            throw new DalDoesNotExistException($"Can not read Engineer. Engineer with ID={id} does Not exist");
        }
        return copy;

    }

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

    public void Update(Engineer item)
    {
        Engineer existingItem = DataSource.Engineers.FirstOrDefault(item => item.Id == item.Id);

        if (existingItem == null)
        {
            throw new DalDoesNotExistException($"Can not update Engineer. Engineer with ID={item.Id} does Not exist");
        }

        // Remove the old object from the list
        DataSource.Engineers.Remove(existingItem);

        // Add the updated object to the list
        DataSource.Engineers.Add(item);
    }

    public void Reset()
    {
        DataSource.Engineers = null;//.Clear();
    }

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

        if (engineers == null)
        {
            throw new DalDoesNotExistException($"Can not read all Engineers. The Engineer list is empty");
        }

        return engineers;

    }
}

