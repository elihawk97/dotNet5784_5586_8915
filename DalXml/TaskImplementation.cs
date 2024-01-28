
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
       
            List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);

            int nextId = XMLTools.GetAndIncreaseNextId("data-config", "NextTaskId");
            entity.Id = nextId;
            tasks.Add(entity);

            XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
            return nextId;
    }
    public void Delete(int id)
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

    public DO.Task? Read(int id)
    {
            List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);

            DO.Task? taskToRead = tasks.FirstOrDefault(e => e.Id == id);

            if (taskToRead == null || taskToRead.IsActive == false)
            {
                throw new DalDoesNotExistException($"Task does not exist.");
            }

            return taskToRead;
    }
    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
            List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
            DO.Task? taskToRead = tasks.FirstOrDefault(filter);


            if (taskToRead == null || taskToRead.IsActive == false)
            {
                throw new DalDoesNotExistException($"Task does not exist.");
            }
            
            return taskToRead;
    }
    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
            List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);

            if (tasks.Count == 0)
            {
                throw new DalDoesNotExistException($"XML File is empty");
            }

            // Add a default filter for isActive = true
            Func<DO.Task, bool> isActiveFilter = task => task.IsActive;

            // Apply user-provided filter if any
            if (filter != null)
            {
                isActiveFilter = task => filter(task) && task.IsActive;
            }

            return tasks.Where(isActiveFilter);
    }

    public void Reset()
    {
            List<DO.Task> tasks = new List<DO.Task>();
            XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
    }

    public void Update(DO.Task item)
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
}
