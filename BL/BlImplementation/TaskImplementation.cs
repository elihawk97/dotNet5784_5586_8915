using DO;
using BO;
namespace BlImplementation;

internal class TaskImplementation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void CreateTask(BO.Task task)
    {
        try
        {
            if(task.Name == null)
            {
                throw new BlInvalidTaskCreation("Can't create the Task, name has not been set!");
            }
            DO.Task doTask = taskCreater(task);
            IEnumerable<DO.Dependency> dependencies = from taskInList in task.Dependencies
                                                      select new Dependency(doTask.Id, taskInList.Id);
            IEnumerable<int> ids = from dependency in dependencies
                                   select _dal.Dependency.Create(dependency);
            int id = _dal.Task.Create(doTask);
        }
        catch(InvalidTime ex)
        {
            throw new BlInvalidDateException("There is a problem with the dates of the task.", ex);
        }
    }

    public void DeleteTask(int id)
    {
        try
        {
            _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"There was an error deleting the task with id={id}", ex);
        }
    }

    public BO.Task ReadTask(int id)
    {
        try {
            DO.Task task = _dal.Task.Read(x => x.Id == id);
            BO.Task boTask = taskDo_BO(task);
            return boTask;
        }
        catch (DO.DalDoesNotExistException ex) {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} already exists", ex);
        } 
    }
    public BO.Task ReadTask(Func<DO.Task, int, bool> filter, int engineerId)
    {
        try
        {
            IEnumerable<BO.Task> tasks = from task in _dal.Task.ReadAll()
                                         where (filter(task, engineerId))
                                         select taskDo_BO(task);
            List<BO.Task>taskList = tasks.ToList();
            return taskList[0];
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with these constraints does not exist", ex);
        }
    }

    public IEnumerable<BO.Task> ReadAll(int engineerId)
    {
            try
            {
            IEnumerable<BO.Task> tasks = null;
                if (engineerId != 0)
                {
                    DO.Engineer engineer = _dal.Engineer.Read(engineerId);
                    tasks = from task in _dal.Task.ReadAll()
                                                 where (task.DifficultyLevel <= engineer.EngineerExperience && task.EngineerID == 0)
                                                 select taskDo_BO(task);

                    tasks = from task in tasks
                            let idNum = task.Id
                            from dependency in _dal.Dependency.ReadAll()
                            where ((dependency.DependentTask == idNum && _dal.Task.Read(dependency.DependentTask).IsActive == false) ||
                                    (dependency.DependentTask != idNum))
                            select task;

                    return tasks;
                }

            tasks = from task in _dal.Task.ReadAll()
                    select taskDo_BO(task);
                   return tasks; 

                
            }
            catch(DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException("Can't Read-All The Task list is empty!", ex);
            }
    }

    public void UpdateStartDate(int id, DateTime newStartTime)
    {
        try
        {
            DO.Task task = _dal.Task.Read(x => x.Id == id);
            if (newStartTime < _dal.getProjectStartDate() || newStartTime > _dal.getProjectEndDate())
            {
                throw new BlInvalidDateException("Date entered violates the time window of the project!");
            }
            if (task.NickName == null)
            {
                throw new BlDoesNotExistException("Task does not exist!");
            }
            task.ActualStartTime = newStartTime; //Change the date of the task
            _dal.Task.Update(task);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlInvalidDateException("Date entered is invalid!", ex);
        }
        catch(DO.InvalidTime ex)
        {
            throw new BlInvalidDateException("Date entered is invalid!", ex);

        }
    }

    public void UpdateTask(int id, BO.Task boTask)
    {
        /// TODO have this ask for the proper fields and actaully update the object
        /// right now this just deletes and adds the same object
        try
        {
            DO.Task doTask = _dal.Task.Read(x => x.Id == id);
            DO.Task doNewTask = taskCreater(boTask);
            _dal.Task.Delete(id);
            _dal.Task.Update(doNewTask);
        }
        catch(DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Can't update Task with id={id}, no such Task exists!", ex);
        }
    }

    public void Reset()
    {
        _dal.Task.Reset();
    }

    private BO.Task taskDo_BO(DO.Task task)
    {
        DO.Engineer engineer;
        BO.Engineer engineerForBO;
        try
        {
             engineer = _dal.Engineer.Read(x => x.Id == task.EngineerID);
            engineerForBO = new BO.Engineer(engineer.Id, engineer.Name, engineer.Email,
            (BO.Enums.ExperienceLevel)engineer.EngineerExperience, engineer.Cost);
        }
        catch (DalDoesNotExistException ex)
        { engineerForBO = null;     }

        
        BO.Task boTask = new BO.Task()
        {
            Id = task.Id,
            Name = task.NickName,
            Description = task.Description,
            DateCreated = task.DateCreated,
            ProjectedStartDate = task.ProjectedStartDate,
            ProjectedEndDate = task.ProjectedStartDate + task.Duration,
            ActualStartDate = task.ActualStartTime,
            DeadLine = task.DeadLine,
            ActualEndDate = task.ActualEndDate,
            Deliverable = task.Deliverables,
            EngineerForTask = engineerForBO,//get engineer based off of the ID
            Level = (BO.Enums.ExperienceLevel)task.DifficultyLevel,
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
            throw new BlInvalidDateException("There is an error with dates entered");
        }
        if (duration > _dal.getProjectEndDate() - task.ActualStartDate)
        {
            throw new BlInvalidDateException("The task takes too long, it will finish after the project's" +
                " final start date");
        }
        if(task.EngineerForTask.Level!=null && task.Level > task.EngineerForTask.Level)
        {
            throw new BlEngineerCantTakeTask($"Engineer with ID={task.EngineerForTask.Id} expertise level is not high enough.");
        }
        DO.Task doTask = new DO.Task(task.Id, task.Name, task.Description, task.DateCreated,
        task.ProjectedStartDate, task.ActualStartDate, duration, task.DeadLine, task.ActualEndDate,
            task.Deliverable, task.Notes, task.EngineerForTask.Id, doExperienceLevel);

        return doTask;
    }







}



