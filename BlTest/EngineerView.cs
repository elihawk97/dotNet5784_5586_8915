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
            int engineerId;
            Console.WriteLine("Enter Your ID:");
            string inputString = Console.ReadLine();
            int.TryParse(inputString, out engineerId);

            Console.WriteLine($@"Choose one of the following {typeof(T).Name}s to perform:
                        0: Exit sub-menu
                        1: Read {typeof(T).Name}
                        2: Read all {typeof(T).Name}s
                        3: Choose a Task");

            inputString = Console.ReadLine();
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
                Program.s_bl.Engineer.UpdateEngineer(engineer);
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
