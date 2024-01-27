
using DO;
using DalApi;
using Dal;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DalXml;

internal class TaskImplementation : ITask
{
    readonly string s_tasks_xml = "tasks";

    public int Create(DO.Task entity)
    {
        try
        {
            List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);

            int nextId = XMLTools.GetAndIncreaseNextId("data-config", "NextTaskId");
            entity.Id = nextId;
            tasks.Add(entity);

            XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
            return nextId;
        }
        catch (DalXMLFileLoadCreateException ex)
        {
            Console.WriteLine($"Error creating task: {ex.Message}");
            return -1; // Or any other value indicating a failure.
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error creating task: {ex.Message}");
            return -1; // Or any other value indicating a failure.
        }
    }
    public void Delete(int id)
    {


        try
        {
            List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);

            DO.Task? taskToDelete = tasks.FirstOrDefault(e => e.Id == id);

            if (taskToDelete == null)
            {
                throw new DalDoesNotExistException($"Task with ID {id} does not exist.");
            }

            taskToDelete.IsActive = false;

            XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
        }

        catch (DalXMLFileLoadCreateException ex)
        {
            Console.WriteLine($"Error creating task: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error creating task: {ex.Message}");
        }
    }

    public DO.Task? Read(int id)
    {
        try
        {
            List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);

            DO.Task? taskToRead = tasks.FirstOrDefault(e => e.Id == id);

            if (taskToRead == null)
            {
                throw new DalDoesNotExistException($"Task with ID {id} does not exist.");
            }

            return taskToRead;

        }

        catch (DalXMLFileLoadCreateException ex)
        {
            Console.WriteLine($"Error creating task: {ex.Message}");
            return null; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error creating task: {ex.Message}");
            return null; 
        }
    }
    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        try
        {
            List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
            DO.Task? taskToRead = tasks.FirstOrDefault(filter);

            if (taskToRead == null)
            {
                throw new DalDoesNotExistException($"Task does not exist.");
            }

            return taskToRead;
        }

        catch (DalXMLFileLoadCreateException ex)
        {
            Console.WriteLine($"Error creating task: {ex.Message}");
            return null; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error creating task: {ex.Message}");
            return null; 
        }
    }
    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        try
        {
            List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);

            if (tasks.Count == 0)
            {
                throw new DalDoesNotExistException($"XML File is empty");
            }

            return filter != null ? tasks.Where(filter) : tasks;
        }
        catch (DalXMLFileLoadCreateException ex)
        {
            Console.WriteLine($"Error creating task: {ex.Message}"); 
                return null ;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error creating task: {ex.Message}");
            return null; 
        }
    }

    public void Reset()
    {
        try
        {
            List<DO.Task> tasks = new List<DO.Task>();
            XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
        }

        catch (DalXMLFileLoadCreateException ex)
        {
            Console.WriteLine($"Error creating task: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error creating task: {ex.Message}");
        }
    }

    public void Update(DO.Task item)
    {
        try
        {
            List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);

            DO.Task? existingItem = tasks.FirstOrDefault(e => e.Id == item.Id);


            if (existingItem == null)
            {
                throw new DalDoesNotExistException($"Can not update Task. Task with ID={item.Id} does not exist");
            }

            tasks.Remove(existingItem);
            tasks.Add(existingItem);

            XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
        }
        catch (DalXMLFileLoadCreateException ex)
        {
            Console.WriteLine($"Error creating task: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error creating task: {ex.Message}");
        }

    }
}
