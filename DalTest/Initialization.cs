using DO;
using DalApi;
using System;
namespace DalTest;


public static class Initialization
{
     static IDal? s_dal;


     static readonly Random s_rand = new();

    public static void Do()
    {
        // Assign the arguments to access variables
        /*        s_dalTask = dalTask ?? throw new NullReferenceException("DAL Task cannot be null!");
                s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL Engineer cannot be null!");
                s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL Dependency cannot be null!");
        */
        s_dal = DalApi.Factory.Get ?? throw new NullReferenceException("DAL object can not be null!");

        // Reset all of the Lists, used for the XML files to reset them for each test run
        s_dal!.Task.Reset();
        s_dal!.Dependency.Reset();
        s_dal!.Engineer.Reset();

        // Call the private initialization methods
        createTasks();
        CreateEngineers();
        CreateDependencies();
    }

    private static void createTasks()
    {
        // Creates 20 Names for Task
        string name = "Task";
        string[] TaskNames = new string[20];

        for (int i = 1; i <= 20; i++)
        {
            TaskNames[i - 1] = $"{name} {i}";
        }

        /// Always start the project on January 1st
        DateTime startingPoint = new DateTime(DateTime.Now.Year, 1, 1);

        // Loop through each month
        for (int month = 1; month <= 4; month++)
        {
            // Create 5 tasks in this month
            for (int i = 1; i <= 5; i++)
            {
                // Random number of days for each task
                int randomAmountOfDays = s_rand.Next(1, 10);

                // Project is Created within at least one year of the current date
                DateTime dateCreated = startingPoint.AddDays(randomAmountOfDays);

                // Projected start date is within the same month
                DateTime? projectedStartDate = null;

                // Project is actually started at most 15 days after the projected start date
                DateTime? actualStartTime = null;

                // Project deadline is between 30 and 60 days after the projected start date
                DateTime deadLine = dateCreated.AddDays(500);

                // Project is actually completed at most 10 days before the deadline
                DateTime? actualEndDate = null;
                TimeSpan? duration = TimeSpan.FromDays(s_rand.Next(8, 12));

                // Setting the difficulty level
                Enums.ExperienceLevel experienceLevel = randomExperienceLevel();

                // Nullable values
                string? description = null;
                string? deliverables = null;
                string? notes = null;
                int? EngineerID = null;

                // Constructor for Task 
                try
                {
                    DO.Task newTask = new(
                        TaskNames[(month - 1) * 5 + i - 1],
                        description,
                        dateCreated,
                        projectedStartDate,
                        actualStartTime,
                        duration,
                        deadLine,
                        actualEndDate,
                        deliverables,
                        notes,
                        EngineerID,
                        experienceLevel);
                    s_dal!.Task.Create(newTask);
                }
                catch (InvalidTime ex)
                {
                    Console.WriteLine(ex);
                }
            }

        }
    }

    private static void CreateEngineers()
    {
        string[] EngineerNames = { "Ariel Blumstein", "Binyamin Klein", "Yishai Dredzen", "Avi Soclof" , "Eli Hawk" }; 

        foreach (var EngineerName in EngineerNames){

/*            do
                 _id = s_rand.Next(EngineerName.Length);
            while (s_dal!.Engineer.Read(_id) != null);*/
           
            //setting the Email
            string email = ""; 
            string[] parts = EngineerName.Split(' ');
            if (parts.Length >= 2)
            {
                string firstNameInitial = parts[0].Substring(0, 1);
                string lastName = parts[1];

                string modifiedName = $"{firstNameInitial}. {lastName}";
                email = modifiedName + "@jct.com";
            }

            //set the Experience Level 
           
            Enums.ExperienceLevel experienceLevel = randomExperienceLevel();
             

            //generate Cost
            double Cost = s_rand.NextDouble() * (300 - 150) + 150;

            //Create new Engineer Object
            Engineer NewEngineer = new(
                EngineerName, 
                email, 
                experienceLevel, 
                Cost);

            //Create using Crud method Create
            s_dal!.Engineer.Create(NewEngineer);

        }
    }

    private static void CreateDependencies()
    {
        // Get all tasks from the database
        List<DO.Task> tasks = s_dal!.Task.ReadAll().ToList();

        // Filter out tasks that are not in the first month (assuming tasks are ordered by month)
        List<DO.Task> firstMonthTasks;
        List<DO.Task> currentMonthTasks;
        // Iterate over each task starting from the second month
        for (int month = 1; month <= 4; month++)
        {
            try
            {
                // Filter out tasks that are not in the last month (assuming tasks are ordered by month)
                firstMonthTasks = tasks.Where(task => task.Id <= month*5 && task.Id >= (month - 1) * 5).ToList();

                // Get tasks for the current month
                currentMonthTasks = tasks.Where(task => task.Id <= (month+1) * 5 && task.Id > (month) * 5).ToList();

            // Determine the number of dependencies for tasks in the current month
            int numDependencies = s_rand.Next(1, 4);

            // Iterate over tasks in the current month to create dependencies
            foreach (var currentTask in currentMonthTasks)
            {
                    numDependencies = s_rand.Next(1, 4);
                    // Randomly select tasks from the previous month as dependencies
                    for (int i = 0; i < numDependencies; i++)
                {
                    // Randomly select a task from the previous month
                    DO.Task dependencyTask = firstMonthTasks[s_rand.Next(0, firstMonthTasks.Count)];

                    // Create dependency
                    Dependency newDependency = new Dependency(currentTask.Id, dependencyTask.Id);

                    // Check for circular dependency
/*                    if (!checkCircularDependency(newDependency))
                    {*/
                        // Create the dependency using CRUD method Create
                        s_dal!.Dependency.Create(newDependency);
                   // }
                }
            }
            }
            catch (DalDoesNotExistException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    /// <summary>
    /// Checks for circular dependencies starting from the given dependency item.
    /// </summary>
    /// <param name="item">The dependency item to check for circular dependencies.</param>
    /// <returns>True if a circular dependency is detected, otherwise false.</returns>
    static bool checkCircularDependency(DO.Dependency item)
    {
        //If the dependent task is the same as the task it depends on, it's circular.

        if (item.DependentTask == item.DependentOnTask)
        {
            return true;
        }

      

        // Recursive helper function to check circular dependencies within the dependency chain.
        bool checkCircularHelper(DO.Dependency item, int dependentID)
        {
            List<DO.Dependency> chain = new List<DO.Dependency>();
            bool res;
            chain = (List<Dependency>)s_dal!.Dependency.ReadAll(i => i.DependentTask == item.DependentOnTask);
            foreach (var d in chain)
            {
                if (d.DependentOnTask == dependentID)
                    return true;
                res = checkCircularHelper(d, dependentID);
                if (res) return res;
            }
            return false;
        }
        return checkCircularHelper(item, item.DependentTask);
    }

    /// <summary>
    ///  Provides a random experience level from enum ExperienceLevel 
    /// </summary>
    /// <returns>Enum.Experience Level</returns>
    static Enums.ExperienceLevel randomExperienceLevel() { 
    int level = s_rand.Next(1, 6);
    Enums.ExperienceLevel DifficultyLevel;
            switch (level)
            {
                case 1:
                    DifficultyLevel = Enums.ExperienceLevel.Novice;
                    break;
                case 2:
                    DifficultyLevel = Enums.ExperienceLevel.AdvancedBeginner;
                    break;
                case 3:
                    DifficultyLevel = Enums.ExperienceLevel.Competent;
                    break;
                case 4:
                    DifficultyLevel = Enums.ExperienceLevel.Proficient;
                    break;
                case 5:
                    DifficultyLevel = Enums.ExperienceLevel.Expert;
                    break;
                default:
                    DifficultyLevel = Enums.ExperienceLevel.Novice;
                break; 
            }
        return DifficultyLevel; 
    }
}

