﻿using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BlTest
{
    internal class AdminView
    {

        public static void AdminTaskPlanning<T>()
        {
          
            Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
            if (ans == "Y")
            {
                DalTest.Initialization.Do();
            }
            Console.WriteLine($@"Choose one of the following {typeof(T).Name}s to perform:
                        0: Exit sub-menu
                        1: Create {typeof(T).Name}
                        2: Read {typeof(T).Name}
                        3: Update {typeof(T).Name}
                        4: Delete {typeof(T).Name}
                        5: Read all {typeof(T).Name}s
                        6: Update {typeof(T).Name}'s Projected Start Date
                        7: Add Dependencies
                        8: Reset
                        Any Other number to go back");

            String inputString = Console.ReadLine();
            int choice;
            bool success = int.TryParse(inputString, out choice);
            while (choice != 0)
            {
                if (success)
                {
                    switch (choice)
                    {
                        case 1:
                            Program.CreateEntity<T>();
                            break;
                        case 2:
                            Program.ReadEntity<T>();
                            break;
                        case 3:
                            Program.UpdateEntity<T>();
                            break;
                        case 4:
                            Program.DeleteEntity<T>();
                            break;
                        case 5:
                            Program.ReadAllEntities<T>();
                            break; 
                        case 6:
                            Program.UpdateEntityProjectedStartDate<T>();
                            break;
                        case 7:
                            AddDependencies();
                            break;
    
                        case 8:
                            Program.ResetEntities<T>();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Request Failed.");
                }
                Console.WriteLine($@"Choose one of the following {typeof(T).Name}s to perform:
                        0: Exit sub-menu
                        1: Create {typeof(T).Name}
                        2: Read {typeof(T).Name}
                        3: Update {typeof(T).Name}
                        4: Delete {typeof(T).Name}
                        5: Read all {typeof(T).Name}s
                        6: Update {typeof(T).Name}'s Projected Start Date
                        7: Add Dependencies
                        8: Reset
                        Any Other number to go back"); inputString = Console.ReadLine();
                success = int.TryParse(inputString, out choice);
            }




        }

        public static void AdminChooseEntity()
        {
            Console.WriteLine(@"Please choose an entity to interact with:
                            1: Engineers 
                            2: Tasks");


            int view = int.Parse(Console.ReadLine());
            while (view != 0)
            {
                switch (view)
                {
                    case 1:
                        AdminViewEngineer<BO.Engineer>();
                        break;
                    case 2:
                        AdminTaskProduction<BO.Task>();
                        break;
                    default:
                        Console.WriteLine("Choice entered is invalid, please enter a valid option.");
                        break;
                }


            }
        }

            public static void AdminTaskProduction<T>()
            {
           

            Console.WriteLine($@"Choose one of the following {typeof(T).Name}s to perform:
                        0: Exit sub-menu
                        1: Read {typeof(T).Name}
                        2: Read all {typeof(T).Name}s
                        3: Assign Engineer to Task
                        4: Update fields that don't change the dates
                        Any Other number to go back");

            string inputString = Console.ReadLine();
            int choice;
            bool success = int.TryParse(inputString, out choice);
            while (choice != 0)
            {
                if (success)
                {
                    switch (choice)
                    {
                        case 1:
                            Program.ReadEntity<T>();
                            break;
                        case 2:
                            Program.ReadAllEntities<T>();
                            break;
                        case 3:
                            AssignEngineerToTask();
                            break;
                        case 4:
                            Program.UpdateEntity<T>();
                            break;
                        default:
                            AdminChooseEntity();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Request Failed.");
                }
                Console.WriteLine($@"Choose one of the following {typeof(T).Name}s to perform:
                        0: Exit sub-menu
                        1: Read {typeof(T).Name}
                        2: Read all {typeof(T).Name}s
                        3: Assign Engineer to Task
                        4: Update fields that don't change the dates
                        Any Other number to go back"); inputString = Console.ReadLine();
                success = int.TryParse(inputString, out choice);
            }
        }

        public static void AdminViewEngineer<T>()
        {
            Console.WriteLine($@"Choose one of the following {typeof(T).Name}s to perform:
                        0: Exit sub-menu
                        1: Create {typeof(T).Name}
                        2: Read {typeof(T).Name}
                        3: Update {typeof(T).Name}
                        4: Delete {typeof(T).Name}
                        5: Read all {typeof(T).Name}s
                        8: Find Engineer's Task Assignment
                        6: Reset
                        Any Other number to go back");

            string inputString = Console.ReadLine();
            int choice;
            bool success = int.TryParse(inputString, out choice);
            while (choice != 0)
            {
                if (success)
                {
                    switch (choice)
                    {
                        case 1:
                            Program.CreateEntity<T>();
                            break;
                        case 2:
                            ReadEngineer();
                            break;
                        case 3:
                            Program.UpdateEntity<T>();
                            break;
                        case 4:
                            Program.DeleteEntity<T>();
                            break;
                        case 5:
                            ReadAllEngineers();
                            break;
                        case 6:
                            Program.ResetEntities<T>();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Request Failed.");
                }
                Console.WriteLine($@"Choose one of the following {typeof(T).Name}s to perform:
                        0: Exit sub-menu
                        1: Create {typeof(T).Name}
                        2: Read {typeof(T).Name}
                        3: Update {typeof(T).Name}
                        4: Delete {typeof(T).Name}
                        5: Read all {typeof(T).Name}s
                        8: Find Engineer's Task Assignment
                        6: Reset
                        Any Other number to go back"); inputString = Console.ReadLine();
                success = int.TryParse(inputString, out choice);
            }
        }

        private static void ReadEngineer()
        {
            try
            {
                Console.WriteLine("What is the ID of the Engineer you wish to query?");
                int engineerId;
                string inputString = Console.ReadLine();
                int.TryParse(inputString, out engineerId);
                BO.Engineer engineer = Program.s_bl.Engineer.ReadEngineer(engineerId);
                Func<DO.Task, int, bool> filterTask = (task, engineerId) => task.EngineerID == engineer.Id;

                Console.WriteLine(Program.s_bl.Engineer.ReadEngineer(engineer.Id)); 
                Console.WriteLine("Assigned to Task: " + Program.s_bl.Task.ReadTask(filterTask, engineer.Id));
            }
            catch (BlDoesNotExistException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private static void ReadAllEngineers()
        {
            Console.WriteLine(@"Enter the Level of Engineers you wish to view from the following list:
                    Novice,
                    AdvancedBeginner,
                    Competent,
                    Proficient,
                    Expert
            ");
            try
            {
                String input = Console.ReadLine();
                input = input.ToUpperInvariant(); // Case-insensitive comparison

                Enum.TryParse<BO.Enums.ExperienceLevel>(input, true, out var level);
                Func<BO.Engineer, bool> filter = (engineer) => engineer.Level == level;
                Func<DO.Task, int, bool> filterTask = (task, engineer) => task.EngineerID == 0;

                IEnumerable<BO.Engineer> engineers = Program.s_bl.Engineer.ReadAll(filter);
                int engineerId;

                foreach (BO.Engineer engineer in engineers)
                {
                    engineerId = engineer.Id;
                    Console.WriteLine(engineer);
                    filterTask = (task, engineerId) => task.EngineerID == engineerId;

                    Console.WriteLine("Assigned to Task: " + Program.s_bl.Task.ReadTask(filterTask, engineer.Id));
                }
            }
            catch (BlDoesNotExistException ex)
            {
                Console.WriteLine(ex);
            }
            catch (BlNullPropertyException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void AssignEngineerToTask()
        {
            Console.WriteLine("What is the ID of the Task you wish to assign to an Engineer");
            int taskId;
            string inputString = Console.ReadLine();
            int.TryParse(inputString, out taskId);

            Console.WriteLine("What is the ID of the Task you wish to assign to an Engineer");
            int engineerId;
            inputString = Console.ReadLine();
            int.TryParse(inputString, out engineerId);

            try
            {
                BO.Task task = Program.s_bl.Task.ReadTask(taskId);
                BO.Engineer engineer = Program.s_bl.Engineer.ReadEngineer(engineerId);
                engineer.Task = task;   
                task.EngineerForTask = engineer;
                Program.s_bl.Task.UpdateTask(taskId, task);
                Program.s_bl.Engineer.UpdateEngineer(engineerId, engineer);
            }
            catch (BlDoesNotExistException ex)
            {
                Console.WriteLine(ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void AddDependencies()
        {
            try
            {
                Console.WriteLine("Enter the ID of the Task you wish to add dependencies to:");
                if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a positive numeric ID.");
                    return;
                }

                BO.Task curTask = Program.s_bl.Task.ReadTask(id);
                if (curTask.Dependencies == null)
                {
                    curTask.Dependencies = new List<TaskInList>();
                }

                Console.WriteLine($"Enter the IDs of the Tasks you wish to add to task {id}'s dependency list, 0 to stop:");
                int taskId;
                while (true)
                {
                    string inputString = Console.ReadLine();
                    if (!int.TryParse(inputString, out taskId) || taskId == 0)
                    {
                        break; // Exit the loop if the user enters '0' or invalid input
                    }

                    // Prevent adding a dependency to itself
                    if (taskId == id)
                    {
                        Console.WriteLine("A task cannot depend on itself. Please enter a different ID.");
                        continue;
                    }

                    // Check if the dependency already exists
                    if (curTask.Dependencies.Any(d => d.Id == taskId))
                    {
                        Console.WriteLine($"Task {taskId} is already listed as a dependency.");
                        Console.WriteLine("Please enter a different ID"); 
                        continue; // Skip the rest of the loop and do not add this dependency
                    }

                    // Additional checks can be performed here, such as verifying the task exists
                    try
                    {
                        Program.s_bl.Task.ReadTask(taskId); // This will throw an exception if the task doesn't exist

                        Console.WriteLine("Enter the name of this dependency:");
                        string nameInput = Console.ReadLine();

                        Console.WriteLine("Enter the Description of this dependency:");
                        string descriptionInput = Console.ReadLine();

                        // Add the dependency since it passed all checks
                        curTask.Dependencies.Add(new TaskInList(taskId, descriptionInput, nameInput, BO.Enums.TaskStatus.Unscheduled));
                        Console.WriteLine($"Added dependency to task {id}.");
                    }
                    catch (BlDoesNotExistException ex)
                    {
                        Console.WriteLine($"Cannot add dependency: {ex.Message}");
                    }
                }

                // Update the task with its new dependencies
                Program.s_bl.Task.UpdateTask(id, curTask);
            }
            catch (BlDoesNotExistException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
