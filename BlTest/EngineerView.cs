using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlTest
{
    internal class EngineerView
    {
        public static void EngineerViewProduction<T>()
        {

            Func<string, (bool, int)> tryParseAndValidateEngineer = (input) =>
            {
                if (int.TryParse(input, out int engineerId))
                {
                    try
                    {
                        Program.s_bl.Engineer.ReadEngineer(engineerId);
                        return (true, engineerId); // Engineer exists, return true and the ID
                    }
                    catch (BlDoesNotExistException)
                    {
                        Console.WriteLine($"No engineer found with ID {engineerId}. Please try again.");
                        // Engineer does not exist, return false to keep the loop running
                    }
                }
                // Parsing failed, return false to indicate invalid input
                return (false, 0);
            };

            int engineerId = Program.GetUserInput<int>("Enter Your ID:", tryParseAndValidateEngineer);


          
            


            try
            {
                Program.s_bl.Engineer.ReadEngineer(engineerId);
            }
            
            catch (BlDoesNotExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
        
                Console.WriteLine($@"Choose one of the following {typeof(T).Name}s to perform:
                        0: Exit sub-menu
                        1: Read {typeof(T).Name}
                        2: Read all {typeof(T).Name}s
                        3: Choose a Task");

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
                                ReadAllTasks(engineerId);
                                break;
                            case 3:
                                AssignToTask(engineerId);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Request Failed.");
                    }
                    Console.WriteLine("Select a choice from the menu:");
                    inputString = Console.ReadLine();
                    success = int.TryParse(inputString, out choice);
                }
            
           
        }


        private static void AssignToTask(int engineerId)
        {
            Console.WriteLine("Enter the ID of the Task you wish to choose:");
            int taskId;
            string inputString = Console.ReadLine();
            int.TryParse(inputString, out taskId);

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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Read all tasks that the engineer can be assigned to.
        /// </summary>
        /// <param name="engineerId">The Engineer's ID</param>
        public static void ReadAllTasks(int engineerId)
        {
            try
            {
                Program.s_bl.Task.ReadAll(engineerId);
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

    }
}
