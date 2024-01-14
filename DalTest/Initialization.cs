﻿namespace DalTest;

using DO;
using DalApi;
using Dal;
using System;
using System.Net;
using System.Data.Common;


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

        // Creates 20 Names for Task
        string name = "Dependency";
        string[] TaskNames = new string[20];

        for (int i = 1; i <= 20; i++)
        {
            TaskNames[i - 1] = $"{name} {i}";
        }
        

        foreach (var Task in TaskNames)
        {

            //Setting the Id
            int _id;
            do
                _id = s_rand.Next(TaskNames.Length);
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
            Enums.ExperienceLevel experienceLevel = randomExperienceLevel();

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
                experienceLevel);

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
           
            Enums.ExperienceLevel experienceLevel = randomExperienceLevel();
             

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
        string[] DependencyNames = new string[40];

        for (int i = 1; i <= 40; i++)
        {
            DependencyNames[i - 1] = $"{name} {i}";
        }

        int count = 0;
        foreach (var dependencyName in DependencyNames)
        {
            // Setting the Dependency Id
            int dependencyId;
            do
            {
                dependencyId = s_rand.Next(DependencyNames.Length);
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
            DateTime createdOn = DateTime.Now.AddDays(-s_rand.Next(1, 357));
           

            // Creating Dependency object
            Dependency newDependency = new Dependency
            (
                dependencyId,
                dependentTaskId,
                dependentOnTaskId,
                customerEmail,
                createdOn
            );


            if (count > 0)
            {
                if (checkCircularDependency(newDependency))
                {
                    // Circular dependency detected, skip this iteration
                    continue;
                }
            }
          
            // Creating using CRUD method Create
            s_dalDependency!.Create(newDependency);
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

