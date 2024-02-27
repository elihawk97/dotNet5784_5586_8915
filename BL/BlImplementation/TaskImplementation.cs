using DO;
using BO;
namespace BlImplementation;

/// <summary>
/// Implements task-related business logic.
/// Interacts with the DAL for data access and manipulation.
/// </summary>
internal class TaskImplementation : BlApi.ITask
{
    /// <summary>
    /// DAL instance for data access.
    /// </summary>
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Creates a new task in the system.
    /// </summary>
    /// <param name="task">The task data to create.</param>
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

    /// <summary>
    /// Deletes a task with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the task to delete.</param>
    public void DeleteTask(int id)
    {
        try
        {
          
            _dal.Task.Read(x => x.Id == id);
            
            _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"There was an error deleting the task with id={id}", ex);
        }
    }

    /// <summary>
    /// Reads a task with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the task to read.</param>
    /// <returns>The retrieved task.</returns>
    public BO.Task ReadTask(int id)
    {
        try {
            DO.Task task = _dal.Task.Read(x => x.Id == id);
            BO.Task boTask = taskDo_BO(task);
            return boTask;
        }
        catch (DO.DalDoesNotExistException ex) {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does not exist", ex);
        } 
    }

    /// <summary>
    /// Reads a task using a filter function and engineer ID.
    /// </summary>
    /// <param name="filter">The filter function to apply.</param>
    /// <param name="engineerId">The ID of the engineer to filter by.</param>
    /// <returns>The retrieved task.</returns>
    public BO.Task? ReadTask(Func<DO.Task, int, bool> filter, int engineerId)
    {
        try
        {
            IEnumerable<BO.Task> tasks = from task in _dal.Task.ReadAll()
                                         where (filter(task, engineerId))
                                         select taskDo_BO(task);
            List<BO.Task>taskList = tasks.ToList();

            if (taskList.Count == 0)
            {
                return null; 
            }

            return taskList[0];
        }


        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with these constraints does not exist", ex);
        }
    }


    /// <summary>
    /// Reads all tasks, optionally filtering by engineer ID and considering dependencies.
    /// </summary>
    /// <param name="engineerId">(Optional) The ID of the engineer to filter tasks for.</param>
    /// <returns>An IEnumerable of retrieved tasks.</returns>
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

    /// <summary>
    /// Updates the start date of a task with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the task to update.</param>
    /// <param name="newStartTime">The new start date.</param>
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
            task.ProjectedStartDate = newStartTime; //Change the date of the task
            _dal.Task.Update(task);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException("Task does not exist!", ex);
        }
        catch(DO.InvalidTime ex)
        {
            throw new BlInvalidDateException("Date entered is invalid!", ex);

        }
    }

    /// <summary>
    /// Updates a task with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the task to update.</param>
    /// <param name="boTask">The updated task data.</param>
    public void UpdateTask(int id, BO.Task boTask)
    {
        try
        {
            DO.Task doTask = _dal.Task.Read(x => x.Id == id);
            DO.Task doNewTask = taskCreater(boTask);
            doNewTask.Id = id; 
            _dal.Task.Update(doNewTask);
        }
        catch(DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Can't update Task with id={id}, no such Task exists!", ex);
        }
    }

    /// <summary>
    /// Resets task data (likely clears or repopulates the data source).
    /// </summary>
    public void Reset()
    {
        _dal.Task.Reset();
    }

    /// <summary>
    /// Converts a DO.Task object to a BO.Task object.
    /// </summary>
    /// <param name="task">The DO.Task object to convert.</param>
    /// <returns>The converted BO.Task object.</returns>
    public BO.Task taskDo_BO(DO.Task task)
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

        Func<DO.Dependency, bool> depFilter = dependency => dependency.DependentTask == task.Id;
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
            Dependencies = _dal.Dependency.ReadAll(depFilter)
            .Select(dep => new TaskInList(dep.DependentOnTask, null, null, BO.Enums.TaskStatus.Unscheduled)).ToList(),
        EngineerForTask = engineerForBO,//get engineer based off of the ID
            Level = (BO.Enums.ExperienceLevel)task.DifficultyLevel,
            Notes = task.Notes,
            RequiredEffortTime = task.Duration
        };

        return boTask;
    }


    /// <summary>
    /// Creates a DO.Task object from a BO.Task object, performing validation.
    /// </summary>
    /// <param name="task">The BO.Task object to create a DO.Task from.</param>
    /// <returns>The created DO.Task object.</returns>
    private DO.Task taskCreater(BO.Task task)
    {
        DO.Enums.ExperienceLevel doExperienceLevel = (DO.Enums.ExperienceLevel)task.Level;

        // Check the dates make sense, note the project may start before the projected
        // start date and finish before the deadline, and may finish after the deadline
       

        DO.Task doTask = new DO.Task(task.Id, task.Name, task.Description, task.DateCreated,
        task.ProjectedStartDate, task.ActualStartDate, task.RequiredEffortTime, task.DeadLine, task.ActualEndDate,
            task.Deliverable, task.Notes, null, doExperienceLevel);

         doTask.EngineerID = task.EngineerForTask.Id;
   
        /// Create Dependency Objects based on the Dependency list
        /// 

    

        if (task.Dependencies == null)
        {
            task.Dependencies = new List<TaskInList>(); // Replace DependencyType with your actual dependency type
        }
        foreach (var dependency in task.Dependencies)
        {
            try
            {
                if (((DateTime)_dal.Task.Read(dependency.Id).ProjectedStartDate).Add((TimeSpan)_dal.Task.Read(dependency.Id).Duration) < task.ProjectedStartDate)
                {
                    _dal.Dependency.Create(new Dependency(task.Id, dependency.Id));
                }
                else
                {
                    throw new DalBadDependency("This task starts before the other task is done");
                }
            }
            catch(DalDoesNotExistException ex)
            {
                Console.WriteLine(ex);
            }
            catch(DalBadDependency ex)
            {
                Console.WriteLine(ex);
            }
        }


        return doTask;
    }


    public void Scheduler()
    {
        List<DO.Task> doTaskList = _dal.Task.ReadAll(null).ToList();
        List<BO.Task> boTaskList = doTaskList.Select(task => taskDo_BO(task)).ToList();
        boTaskList = TopologicalSort(boTaskList);

        // For each task in the sorted list:
        // - Calculate the latest end date among its dependencies.
        // - Set the task's actual start date to one day after the latest dependency end date, or projected start date if it has no dependencies.
        // - Calculate the task's actual end date based on its projected dates.
        // - Check if the actual end date exceeds the task's deadline or project deadline.
        // - If all checks pass, proceed; otherwise, throw an exception.
        foreach (BO.Task task in boTaskList)
        {
            DateTime? latestEndDate = null;
            if (task.Dependencies.Count() != 0)
            {
                latestEndDate = task.Dependencies.Max(dep => (((DateTime)_dal.Task.Read(dep.Id).ProjectedStartDate).Add((TimeSpan)_dal.Task.Read(dep.Id).Duration)));
            }
            if (latestEndDate != null)
            {
                task.ProjectedStartDate = latestEndDate.Value.AddDays(1);
                task.ProjectedEndDate = latestEndDate.Value.Add((TimeSpan)(task.RequiredEffortTime));
            }
            else //If it has no dependencies, we set it start and finish days to be to the ones the
                 // the admin suggested
            {
                task.ProjectedStartDate = task.DateCreated;
                task.ProjectedEndDate = task.DateCreated.Value.Add((TimeSpan)(task.RequiredEffortTime));
            }
            // Check the date is set to finish before the task's deadline and project deadline
            if (task.ProjectedEndDate > task.DeadLine || task.ProjectedEndDate > _dal.getProjectEndDate())
            {
                throw new BlTasksCanNotBeScheduled("The given tasks can not be completed before the deadline.");
            }
            _dal.Task.Update(taskCreater(task));
        }
        foreach (BO.Task task in boTaskList)
        {
            Console.WriteLine(task);
        }
    }


    public static List<BO.Task> TopologicalSort(List<BO.Task> tasks)
    {
        List<BO.Task> sortedTasks = new List<BO.Task>();
        var dependencies = new Dictionary<int, HashSet<int>>();
        int amount = tasks.Count();
        // Populate dependencies dictionary
        foreach (var task in tasks)
        {
            if (task.Dependencies != null && task.Dependencies.Count() != 0)
            {
                dependencies[task.Id] = new HashSet<int>(task.Dependencies.Select(dep => dep.Id));
            }
        }

        var queue = new Queue<BO.Task>(tasks.Where(task => !dependencies.ContainsKey(task.Id) || dependencies[task.Id].Count == 0));

        while (queue.Any())
        {
            var currentTask = queue.Dequeue();
            sortedTasks.Add(currentTask);
            // Remove the current task from the dependencies of other tasks
            foreach (var dep in dependencies.Values)
            {
                dep.Remove(currentTask.Id);
            }
            // Enqueue tasks whose dependencies are satisfied
            List<BO.Task> newTasks = tasks.Where(task => dependencies.ContainsKey(task.Id) && dependencies[task.Id].Count == 0 && !sortedTasks.Any(sortedTask => sortedTask.Id == task.Id)).ToList();
            foreach (var task in newTasks)
            {
                queue.Enqueue(task);
                tasks.RemoveAll(curTask => curTask.Id == task.Id);
            }
        }

        // Check for cycles
        if (sortedTasks.Count != amount)
        {
            throw new Exception("Cycle detected in the task dependencies. Unable to perform topological sort.");
        }

        return sortedTasks;
    }




}



