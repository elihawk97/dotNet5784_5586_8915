using DalApi;
using DO;
using BO;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Reflection.Emit;

namespace BlTest;

internal class Program
{
    public static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public static int curEngineer = 0;


    public static T GetUserInput<T>(string prompt, Func<string, (bool IsValid, T Value)> tryParse)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine() ?? "";
            var (isValid, value) = tryParse(input);
            if (isValid)
            {
                return value;
            }
            Console.WriteLine("Invalid input. Please try again.");
        }
    }





    public static BO.Task taskInput()
    {
        Console.WriteLine("Enter data to create a new task:/n");

        // For string input (e.g., nickName and description)
        string nickName = GetUserInput<string>("Enter name (string):", input =>
        {
            bool isValid = !string.IsNullOrEmpty(input);
            return (isValid, input); // input is directly the value for string types
        });

        // For string input (e.g., nickName and description)
        string description = GetUserInput<string>("Enter description (string):", input =>
        {
            bool isValid = !string.IsNullOrEmpty(input);
            return (isValid, input); // input is directly the value for string types
        });
        
        DateTime dateCreated = DateTime.Now;

        // For DateTime input (e.g., dateCreated, projectedStartDate)
        DateTime projectedStartDate = GetUserInput<DateTime>("Enter Projected Start Date (MM/dd/yyyy):", input =>
        {
            bool isValid = DateTime.TryParseExact(input, "MM/dd/yyyy", null, DateTimeStyles.None, out DateTime value)
                  && value >= DateTime.Today; // Ensure date is not in the past
            return (isValid, value);
        });

        DateTime projectedEndDate = GetUserInput<DateTime>("Enter Projected End Date (MM/dd/yyyy):", input =>
        {
            bool isValid = DateTime.TryParseExact(input, "MM/dd/yyyy", null, DateTimeStyles.None, out DateTime value)
                  && value >= DateTime.Today && value > projectedStartDate; // Ensure date is not in the past
            return (isValid, value);
        });

        // For DateTime input (e.g., dateCreated, projectedStartDate)
        DateTime deadline = GetUserInput<DateTime>("Enter deadline (MM/dd/yyyy):", input =>
        {
            bool isValid = DateTime.TryParseExact(input, "MM/dd/yyyy", null, DateTimeStyles.None, out DateTime value)
                  && value >= DateTime.Today && value >= projectedEndDate; // Ensure date is not in the past
            return (isValid, value);
        });

        string deliverables = GetUserInput<string>("Enter deliverables:", input =>
        {
            bool isValid = !string.IsNullOrEmpty(input);
            return (isValid, input); // input is directly the value for string types
        });

        string notes = GetUserInput<string>("Enter notes:", input =>
        {
            bool isValid = !string.IsNullOrEmpty(input);
            return (isValid, input); // input is directly the value for string types
        });

        // For Enums (e.g., level)
        BO.Enums.ExperienceLevel level = GetUserInput<BO.Enums.ExperienceLevel>("Enter experience level (e.g., Novice, AdvancedBeginner):", input =>
        {
            bool isValid = Enum.TryParse<BO.Enums.ExperienceLevel>(input, out var value);
            return (isValid, value);
        });


        BO.Task newTask = new BO.Task(
            nickName,
            description,
            new List<BO.TaskInList>(), // Dependencies can be added later
            dateCreated,
            projectedStartDate,
            projectedEndDate,
            deadline,
            deliverables,
            notes,
            level
        );

        return newTask;
    }


    public static T GetEntityInput<T>()
    {
        if (typeof(T) == typeof(BO.Task))
        {
            return (T)(object)taskInput();
        }
        else if (typeof(T) == typeof(BO.Engineer))
        {
            return (T)(object)EngineerInput();
        }
        else
        {
            throw new NotSupportedException($"Unsupported entity type: {typeof(T).Name}");
        }
    }
    public static void CreateEntity<T>()
    {

        try
        {
            T newEntity = GetEntityInput<T>();

            if (typeof(T) == typeof(BO.Task))
            {
                s_bl.Task.CreateTask(newEntity as BO.Task);
            }
            else if (typeof(T) == typeof(BO.Engineer))
            {
                s_bl.Engineer.Create(newEntity as BO.Engineer);
            }
            else
            {
                Console.WriteLine($"Unsupported entity type: {typeof(T).Name}");
            }
        }
        catch (InvalidTime ex)
        {
            Console.WriteLine(ex);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public static void ReadEntity<T>()
    {
        Console.WriteLine($"Enter the id of the {typeof(T).Name} you wish to see");
        int id = int.Parse(Console.ReadLine());

        try
        {
            if (typeof(T) == typeof(BO.Task))
            {
                BO.Task? entityToRead = s_bl.Task.ReadTask(id);
                Console.WriteLine(entityToRead);
            }
            else if (typeof(T) == typeof(BO.Engineer))
            {
                BO.Engineer? entityToRead = s_bl.Engineer.ReadEngineer(id);
                Console.WriteLine(entityToRead);
            }
            else
            {
                Console.WriteLine($"Unsupported entity type: {typeof(T).Name}");
            }
        }
        catch (BlDoesNotExistException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public static void ReadAllEntities<T>()
    {
        try
        {
            IEnumerable<T?> itemList;

            if (typeof(T) == typeof(BO.Task))
            {
                itemList = s_bl.Task.ReadAll(curEngineer).Cast<T>();
            }
            else if (typeof(T) == typeof(BO.Engineer))
            {
                itemList = s_bl.Engineer.ReadAll(null).Cast<T>();
            }
            else
            {
                Console.WriteLine($"Unsupported entity type: {typeof(T).Name}");
                return;
            }

            if (itemList.Any())
            {
                Console.WriteLine($"All {typeof(T).Name}s:");
                foreach (var item in itemList)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine($"No {typeof(T).Name}s found.");
            }
        }
        catch (BlDoesNotExistException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    public static void UpdateEntity<T>()
    {
        try
        {
            Console.WriteLine($"Enter the id of the {typeof(T).Name} you wish to update");
            int id = int.Parse(Console.ReadLine());

            

            if (typeof(T) == typeof(BO.Task))
            {
                s_bl.Task.ReadTask(id);
                BO.Task updateItem = GetEntityInput<BO.Task>();
                s_bl.Task.UpdateTask(id, updateItem);
            }
            else if (typeof(T) == typeof(BO.Engineer))
            {
                s_bl.Engineer.ReadEngineer(id);
                BO.Engineer updateItem = GetEntityInput<BO.Engineer>();
                s_bl.Engineer.UpdateEngineer(id, updateItem);
            }
            else
            {
                Console.WriteLine($"Unsupported entity type: {typeof(T).Name}");
            }
        }
        catch (BlDoesNotExistException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);  
        }
    }

    public static void DeleteEntity<T>()
    {
        try
        {
            Console.WriteLine($"Enter the id of the {typeof(T).Name} you wish to delete");
            int id = int.Parse(Console.ReadLine());

            if (typeof(T) == typeof(BO.Task))
            {
                s_bl.Task.DeleteTask(id);
            }
            else if (typeof(T) == typeof(BO.Engineer))
            {
                s_bl.Engineer.DeleteEngineer(id);
            }
            else
            {
                Console.WriteLine($"Unsupported entity type: {typeof(T).Name}");
            }
        }
        catch (BlDoesNotExistException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    public static void ResetEntities<T>()
    {
        try
        {
            if (typeof(T) == typeof(BO.Task))
            {
                s_bl.Task.Reset();
            }
            else if (typeof(T) == typeof(BO.Engineer))
            {
                s_bl.Engineer.Reset();
            }
            else
            {
                Console.WriteLine($"Unsupported entity type: {typeof(T).Name}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }



    public static BO.Engineer EngineerInput()
    {
        Console.WriteLine("Enter data to create a new engineer:");

        string name = GetUserInput<string>("Enter engineer name:", input =>
        {
            if (!string.IsNullOrWhiteSpace(input)) // Ensure the name is not empty or whitespace
            {
                return (true, input);
            }
            return (false, ""); // Default value for string
        });


        string email = GetUserInput<string>("Enter engineer email:", input =>
        {
            // Basic validation for email, can be extended to more complex regex check
            if (!string.IsNullOrWhiteSpace(input) && input.Contains("@"))
            {
                return (true, input);
            }
            return (false, ""); // Default value for string
        });

        BO.Enums.ExperienceLevel? level = GetUserInput<BO.Enums.ExperienceLevel?>("Enter experience level (Novice, AdvancedBeginner, Competent, Proficient, Expert):", input =>
        {
            if (Enum.TryParse<BO.Enums.ExperienceLevel>(input, true, out var result)) // true for ignoreCase
            {
                return (true, result);
            }
            return (false, null); // Default value for nullable ExperienceLevel
        });

        double cost = GetUserInput<double>("Enter engineer cost:", input =>
        {
            if (double.TryParse(input, out double result))
            {
                return (true, result);
            }
            return (false, 0.0); // Default value for double
        });

        BO.Engineer newEngineer = new BO.Engineer(
            0,
            name,
            email,
            level,
            cost
            );

        return newEngineer;
    }

    public static void UpdateEntityProjectedStartDate<T>()
    {
        Console.WriteLine("What is the ID of the Task you wish to update the Projected" +
            " Start Date for?");
        int id;
        string inputString = Console.ReadLine();
        int.TryParse(inputString, out id);

        try
        {
            s_bl.Task.ReadTask(id);

        }
        catch (BlDoesNotExistException ex)
        {
            Console.WriteLine(ex.Message);
            return; 

        }

        DateTime inputDate = GetUserInput<DateTime>("Enter Projected Start Date (MM/dd/yyyy):", input =>
        {
            bool isValid = DateTime.TryParseExact(input, "MM/dd/yyyy", null, DateTimeStyles.None, out DateTime value)
                  && value >= DateTime.Today;  // Ensure date is not in the past
            return (isValid, value);
        });

     
        try
        {
            s_bl.Task.UpdateStartDate(id, inputDate);
        }
        
        catch (BlInvalidDateException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public static void ResetInitialData()
    {

        Console.Write("Do you really wish to Reset? (Y/N)"); //stage 3
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
        if (ans == "Y")
        {
            ResetEntities<DO.Task>();
            ResetEntities<DO.Engineer>();
            ResetEntities<DO.Dependency>();

            DalTest.Initialization.Do(); //TODO
        }
    }



    static void Main(string[] args)
    {

        Console.WriteLine("You are in Planning Mode:");
           
        AdminView.AdminTaskPlanning<BO.Task>();

        DateTime startDate = GetUserInput<DateTime>("Enter Project Start Date (MM/dd/yyyy):", input =>
        {
            bool isValid = DateTime.TryParseExact(input, "MM/dd/yyyy", null, DateTimeStyles.None, out DateTime value)
                  && value >= DateTime.Today; // Ensure date is not in the past
            return (isValid, value);
        });

        Program.s_bl.Tools.SetProjectStartDate(startDate);
        Console.WriteLine("Project Start Date set successfully.");

        s_bl.Tools.CurrentProjectStage = BO.Enums.ProjectStages.Planning;

        Console.WriteLine("You are now in Production Mode");

        Console.WriteLine(@"Please Select Your Role:
                            1: Admin 
                            2: Engineer");

        int view = int.Parse(Console.ReadLine());
        while (view != 0)
        {
            switch (view)
            {
                case 1:
                    AdminView.AdminChooseEntity();
                    break;
                case 2:
                    EngineerView.EngineerViewProduction<BO.Engineer>();
                    break;
                default:
                    Console.WriteLine("Choice entered is invalid, please enter a valid option.");
                    break;
            }

            Console.WriteLine(@"Please Select Your Role:
                            1: Admin 
                            2: Engineer");

            view = int.Parse(Console.ReadLine());

        }
    }


}