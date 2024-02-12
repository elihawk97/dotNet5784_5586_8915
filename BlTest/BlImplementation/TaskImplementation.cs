using BlApi;
using BO;
using DalApi;
using DO;
using System.ComponentModel.Design;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalList;
using System.Security.Cryptography;
namespace BlImplementation;

internal class TaskImplementation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void CreateTask(BO.Task task)
    {
        try
        {
            DO.Task doTask = taskCreater(task);
            IEnumerable<DO.Dependency> dependencies = from taskInList in task.Dependencies
                                                      select new Dependency(doTask.Id, taskInList.Id);
            IEnumerable<int> ids = from dependency in dependencies
                                   select _dal.Dependency.Create(dependency);
            int id = _dal.Task.Create(doTask);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public void DeleteTask(int id)
    {
        _dal.Task.Delete(id); 
    }

    public BO.Task ReadTask(int id)
    {
        DO.Task task = _dal.Task.Read(x => x.Id == id);
        BO.Task boTask = taskDo_BO(task);
        return boTask;
    }

    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter)
    {
        IEnumerable<DO.Task> tasks = _dal.Task.ReadAll();
        IEnumerable<BO.Task> boTasks = from task in tasks
                                          select taskDo_BO(task);
        if (filter != null){
            boTasks = from task in boTasks
                                           where filter(task)
                                           select task;
        }
        return boTasks;
    }

    public void UpdateStartDate(int id, DateTime newStartTime)
    {
        DO.Task task = _dal.Task.Read(x => x.Id == id);
        if(newStartTime >= DalList.DataSource.config.StartDate)
        {

        }
        task.ActualStartTime = newStartTime; //Change the date of the task
        _dal.Task.Delete(id); // Delete the old task with the old date
        _dal.Task.Create(task);
    }

    public void UpdateTask(int id)
    {
        _dal.Task.Update(_dal.Task.Read(x => x.Id == id));
    }


    private BO.Task taskDo_BO(DO.Task task)
    {
        DO.Engineer engineer = _dal.Engineer.Read(x => x.Id == task.EngineerID);
        BO.Engineer engineerForBO = new BO.Engineer(engineer.Id, engineer.Name, engineer.Email,
            (BO.ExperienceLevel)engineer.EngineerExperience, engineer.Cost);
        BO.Task boTask = new BO.Task()
        {
            Id = task.Id,
            Name = task.NickName,
            Description = task.Description,
            DateCreated = task.DateCreated,
            //Milestone = false, FIX
            ProjectedStartDate = task.ProjectedStartDate,
            ProjectedEndDate = task.ProjectedStartDate + task.Duration,
            ActualStartDate = task.ActualStartTime,
            DeadLine = task.DeadLine,
            ActualEndDate = task.ActualEndDate,
            Deliverable = task.Deliverables,
            EngineerForTask = engineerForBO,//get engineer based off of the ID
            Level = (BO.ExperienceLevel)task.DifficultyLevel,
            Notes = task.Notes,
        };

        return boTask;
    }

    private DO.Task taskCreater(BO.Task task)
    {
        TimeSpan duration = (TimeSpan)(task.ActualEndDate - task.ActualStartDate);
        DO.Enums.ExperienceLevel doExperienceLevel = (DO.Enums.ExperienceLevel)task.Level;

        // Check the dates make sence, note the project may start before the projected
        // start date and finish before the deadline, and may finish after the deadline
        if (task.DateCreated <= task.ProjectedStartDate ||
        task.DateCreated <= task.ActualStartDate || task.DateCreated <= task.DeadLine
        || task.ActualStartDate <= task.ActualEndDate)
        {
            throw new Exception BadDates();
        }
        if (duration < projectEndDate - task.ActualStartDate)
        {
            throw new Exception ;
        }
        DO.Task doTask = new DO.Task(task.Id, task.Name, task.Description, task.DateCreated,
        task.ProjectedStartDate, task.ActualStartDate, duration, task.DeadLine, task.ActualEndDate,
            task.Deliverable, task.Notes, task.EngineerForTask.Id, doExperienceLevel);

        return doTask;
    }
}
