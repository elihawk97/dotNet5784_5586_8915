using BlApi;
using BO;
using DO;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void CreateTask(BO.Task task)
    {
        try
        {
            TimeSpan duration = (TimeSpan)(task.ActualEndDate - task.ActualStartDate);
            DO.Enums.ExperienceLevel doExperienceLevel = (DO.Enums.ExperienceLevel)task.Level;

            // Check the dates make sence, note the project may start before the projected
            // start date and finish before the deadline, and may finish after the deadline
            if (task.DateCreated <= task.ProjectedStartDate ||
                task.DateCreated <= task.ActualStartDate || task.DateCreated<= task.DeadLine
                || task.ActualStartDate <= task.ActualEndDate)
            {
                throw new Exception BadDates();
            }

            DO.Task doTask = new DO.Task(task.Id, task.Name, task.Description, task.DateCreated,
                task.ProjectedStartDate, task.ActualStartDate, duration, task.DeadLine, task.ActualEndDate,
                task.Deliverable, task.Notes, task.EngineerForTask.Id, doExperienceLevel);

            int id = _dal.Task.Create(doTask);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public void DeleteTask(int id)
    {

        throw new NotImplementedException();

    }

    public BO.Task ReadTask(int id)
    {
        DO.Task task = _dal.Task.Read(x => x.Id == id);
        DO.Engineer engineer = _dal.Engineer.Read(x => x.Id == task.EngineerID);
        BO.Engineer engineerForBO = new BO.Engineer() { //Initialize the Engineer for this task
            Id = engineer.Id,
            Name = engineer.Name,
            Email = engineer.Email,
            Level = (BO.ExperienceLevel)engineer.EngineerExperience,
            Cost = engineer.Cost
        };
        BO.Task boTask = new BO.Task() {
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

    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter)
    {
        throw new NotImplementedException();
    }

    public void UpdateStartDate(int id, DateTime newStartTime)
    {
        throw new NotImplementedException();
    }

    public void UpdateTask(int id)
    {
        throw new NotImplementedException();
    }
}
