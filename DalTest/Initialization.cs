namespace DalTest;

using DO;
using DalApi;
using Dal;
using System;
using System.Net;


public static class Initialization
{
    private static ITask? s_dalTask; 
    private static IEngineer? s_dalEngineer; 
    private static IDependency? s_dalDependency;

    private static readonly Random s_rand = new();

    public static void Do(ITask? dalTask, IEngineer? dalEngineer, IDependency? dalDependency)
    {
        // Assign the arguments to access variables
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL Task cannot be null!");
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL Engineer cannot be null!");
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL Dependency cannot be null!");

        // Call the private initialization methods
        createTasks();
        CreateEngineers();
        CreateDependencys();
    }
    private static void createTasks()
    {
        string[] TaskName = {"Task 1", "Task 2", "Task 3", "Task 4", "Task 5",

                               "Task 6", "Task 7", "Task 8", "Task 9", "Task 10",

                               "Task 11", "Task 12", "Task 13", "Task 14", "Task 15",

                               "Task 16", "Task 17", "Task 18", "Task 19", "Task 20"
        };

        foreach (var Task in TaskName)
        {

            //Setting the Id
            int _id;
            do
                _id = s_rand.Next(TaskName.Length);
            while (s_dalTask!.Read(_id) != null);

            //setting all the dates

            DateTime Today = DateTime.Now;


            int randomAmountOfDays = s_rand.Next(1, 357);

            //Project is Created within at least one year of the current date
            DateTime dateCreated = DateTime.Now.AddDays(-randomAmountOfDays);

            //Project is projected to start at least one year after the current date
            DateTime projectedStartDate = DateTime.Now.AddDays(randomAmountOfDays);

            //Project is actually started at most 15 days after the projected start date
            DateTime? actualStartTime = projectedStartDate.AddDays(s_rand.Next(1, 16));

            //Project deadline is between 30 and 60 days after the projected start date
            DateTime deadLine = projectedStartDate.AddDays(s_rand.Next(31, 61));

            //Project is actually completed at most 10 days before the deadline
            DateTime? actualEndDate = deadLine.AddDays(-s_rand.Next(1, 11));
            TimeSpan? duration = actualEndDate - actualStartTime;


            //setting the difficuly level
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
                    return;
            }

            //nullable values
            string? description = null;
            string? deliverables = null; 
            string? notes = null;
            int? EngineerID = null;

            //Constructor for Task 
            Task newTask = new(
                _id, 
                Task, 
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
                DifficultyLevel);

            s_dalTask!.Create(newTask);

        }
    }
        private static void CreateEngineers()
    {
        string[] EngineerNames = { "Ariel Blumstein", "Binyamin Klein", "Yishai Dredzen", "Avi Soclof" , "Eli Hawk" }; 

        foreach (var EngineerName in EngineerNames){

            //Setting the Id

            int _id; 
            do
                 _id = s_rand.Next(EngineerName.Length);
            while (s_dalEngineer!.Read(_id) != null);
           
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
            int level = s_rand.Next(1, 6); 
            Enums.ExperienceLevel experienceLevel;
            switch (level)
            {
                case 1:
                    experienceLevel = Enums.ExperienceLevel.Novice;
                    break;
                case 2:
                    experienceLevel = Enums.ExperienceLevel.AdvancedBeginner;
                    break;
                case 3:
                    experienceLevel = Enums.ExperienceLevel.Competent;
                    break;
                case 4:
                    experienceLevel = Enums.ExperienceLevel.Proficient;
                    break;
                case 5:
                    experienceLevel = Enums.ExperienceLevel.Expert;
                    break;
                default:
                    experienceLevel = Enums.ExperienceLevel.Novice;
                    return;
            }

            //generate Cost
            double Cost = s_rand.NextDouble() * (300 - 150) + 150;

            //Create new Engineer Object
            Engineer NewEngineer = new(
                _id, 
                EngineerName, 
                email, 
                experienceLevel, 
                Cost);


            //Create using Crud method Create
            s_dalEngineer!.Create(NewEngineer);

        }

    }

    private static void CreateDependencys()
    {
        // Creates 40 Names for Dependency
        string name = "Dependency";
        string[] names = new string[40];

        for (int i = 1; i <= 40; i++)
        {
            names[i - 1] = $"{name} {i}";
        }

        foreach (var dependencyName in names)
        {
            // Setting the Dependency Id
            int dependencyId;
            do
            {
                dependencyId = s_rand.Next(names.Length);
            } while (s_dalDependency!.Read(dependencyId) != null);

            // Generating random dependencies for the task
            int dependentTaskId = s_rand.Next(1, 21); // Assuming task Ids range from 1 to 20
            int dependentOnTaskId = s_rand.Next(1, 21); // Assuming task Ids range from 1 to 20

            // Ensure that dependentTaskId and dependentOnTaskId are not the same
            while (dependentTaskId == dependentOnTaskId)
            {
                dependentOnTaskId = s_rand.Next(1, 21);
            }

            // Setting other properties
            string? customerEmail = null;
            string? address = null;
            DateTime createdOn = DateTime.Now.AddDays(-s_rand.Next(1, 357));
            DateTime ship = DateTime.Now.AddDays(s_rand.Next(1, 16));
            DateTime delivery = ship.AddDays(s_rand.Next(1, 11));

            // Creating Dependency object
            Dependency newDependency = new Dependency
            (
                dependencyId,
                dependentTaskId,
                dependentOnTaskId,
                customerEmail,
                address,
                createdOn,
                ship,
                delivery
            );

            if (checkCircularDependency(newDependency))
            {
                // Circular dependency detected, skip this iteration
                continue;
            }

            // Creating using CRUD method Create
            s_dalDependency!.Create(newDependency);
        }
    }


    static bool checkCircularDependency(DO.Dependency item)
    {

        if (item.DependentTask == item.DependentOnTask)
        {
            return true;
        }

        bool checkCircularHelper(DO.Dependency item, int dependentID)
        {
            List<DO.Dependency> chain = new List<DO.Dependency>();
            bool res;
            chain = s_dalDependency!.ReadAll().FindAll(i => i.DependentTask == item.DependentOnTask);
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

}

