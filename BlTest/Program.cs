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
    public static BO.Task taskInput()
    {
        Console.WriteLine("Enter data to create a new task:");
        Console.WriteLine("Enter ID integer:");
        int id;
        if (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("Invalid input for dependency ID. Please enter a valid integer.");
            return null;
        }
        Console.WriteLine("Enter name string:");
        string? nickName = Console.ReadLine();
        if (string.IsNullOrEmpty(nickName))
        {
            Console.WriteLine("Nickname cannot be empty. Please enter a valid name.");
            return null;
        }
        Console.WriteLine("Enter description string:");
        string? description = Console.ReadLine();
        if (string.IsNullOrEmpty(description))
        {
            Console.WriteLine("Description cannot be empty. Please enter a valid description.");
            return null;
        }
        Console.WriteLine("Enter isMilestone bool:");
        bool isMilestone;
        if (!bool.TryParse(Console.ReadLine(), out isMilestone))
        {
            Console.WriteLine("Invalid input for milestone flag. Please enter True or False.");
            return null;
        }
        Console.WriteLine("Enter date created MM/dd/yyyy:");
        DateTime dateCreated;
        if (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out dateCreated))
        {
            Console.WriteLine("Invalid date format. Please enter a valid date in MM/dd/yyyy format.");
            return null;
        }
        Console.WriteLine("Enter projectedStartDate MM/dd/yyyy:");
        DateTime projectedStartDate;
        if (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out projectedStartDate))
        {
            Console.WriteLine("Invalid date format. Please enter a valid date in MM/dd/yyyy format.");
            return null;
        }
        Console.WriteLine("Enter actualStartTime MM/dd/yyyy:");
        DateTime actualStartTime;
        string actualStartTimeInput = Console.ReadLine();
        bool timeParsed = DateTime.TryParseExact(actualStartTimeInput, "MM/dd/yyyy", null, DateTimeStyles.None, out actualStartTime);
        if (!string.IsNullOrEmpty(actualStartTimeInput))
        {
            if (!timeParsed)
            {
                Console.WriteLine("Invalid date format. Please enter a valid date in MM/dd/yyyy  format.");
                return null;
            }
        }

        Console.WriteLine("Enter Duration hh:mm:ss format:");
        TimeSpan duration;
        if (!TimeSpan.TryParseExact(Console.ReadLine(), "hh:mm:ss", null, out duration))
        {
            Console.WriteLine("Invalid duration format. Please enter a valid duration in HH:mm:ss format.");
            return null;
        }

        Console.WriteLine("Enter deadline MM/dd/yyyy:");
        DateTime deadline;
        if (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out deadline))
        {
            Console.WriteLine("Invalid date format. Please enter a valid date in MM/dd/yyyy  format.");
            return null;
        }

        Console.WriteLine("Enter actualEndDate MM/dd/yyyy:");
        DateTime actualEndDate;
        if (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out actualEndDate))
        {
            Console.WriteLine("Invalid date format. Please enter a valid date in MM/dd/yyyy  format.");
            return null;
        }
        Console.WriteLine("Enter deliverables:");
        string deliverables = Console.ReadLine();

        Console.WriteLine("Enter notes:");
        string notes = Console.ReadLine();

        int engineerID;
        if (!int.TryParse(Console.ReadLine(), out engineerID))
        {
            Console.WriteLine("Invalid input for engineer ID. Please enter a valid integer.");
            return null;
        }

        BO.Enums.ExperienceLevel level;
        if (!Enum.TryParse<BO.Enums.ExperienceLevel>(Console.ReadLine(), out level))
        {
            Console.WriteLine("Invalid input for experience level. Please enter a valid option.");
            return null;
        }
        BO.Task newTask = new BO.Task(
            nickName,
            description,
            new List<BO.TaskInList>(), // Dependencies can be added later
            dateCreated,
            projectedStartDate,
            actualStartTime,
            deadline,
            actualEndDate,
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
            Console.WriteLine(ex);
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
            Console.WriteLine(ex);
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
                BO.Task updateItem = GetEntityInput<BO.Task>();
                s_bl.Task.UpdateTask(updateItem.Id, updateItem);
            }
            else if (typeof(T) == typeof(BO.Engineer))
            {
                BO.Engineer updateItem = GetEntityInput<BO.Engineer>();
                s_bl.Engineer.UpdateEngineer(updateItem);
            }
            else
            {
                Console.WriteLine($"Unsupported entity type: {typeof(T).Name}");
            }
        }
        catch (DalDoesNotExistException ex)
        {
            Console.WriteLine(ex);
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
            Console.WriteLine(ex);
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
        Console.WriteLine("Enter engineer ID:");
        int id = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter engineer name:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter engineer email:");
        string email = Console.ReadLine();

        Console.WriteLine("Enter experience level (Novice, AdvancedBeginner, Competent, Proficient, Expert):");
        BO.Enums.ExperienceLevel? level = (BO.Enums.ExperienceLevel?)Enum.Parse(typeof(BO.Enums.ExperienceLevel), Console.ReadLine());

        Console.WriteLine("Enter engineer cost:");
        double cost = double.Parse(Console.ReadLine());

        BO.Task? task = null; //Don't assign the engineer to a task at this point


        BO.Engineer newEngineer = new BO.Engineer(
            id,
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

        DateTime inputDate;
        if (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy HH:mm:ss", null, DateTimeStyles.None, out inputDate))
        {
            Console.WriteLine("Invalid date format. Please enter a valid date in MM/dd/yyyy HH:mm:ss format.");
        }

        s_bl.Task.UpdateStartDate(id, inputDate);
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
        Console.WriteLine(@"Choose the Admin/Engineer mode:
                            1: Engineer view 
                            2: Admin Task Planning Mode
                            3: Admin Engineer Mode
                            4: Admin Task Production Mode");
        int view = int.Parse(Console.ReadLine());
        while (view != 0)
        {
            switch (view)
            {
                case 1:
                    EngineerView.EngineerViewProduction<BO.Engineer>();
                    break;
                case 2:
                    AdminView.AdminTaskPlanning<BO.Task>();
                    break;
                case 3:
                    AdminView.AdminViewEngineer<BO.Engineer>();
                    break;
                case 4:
                    AdminView.AdminTaskProduction<BO.Task>();
                    break;
                default:
                    Console.WriteLine("Choice entered is invalid, please enter a valid option.");
                    break;
            }
            Console.WriteLine(@"Choose the Admin/Engineer mode:
                            1: Engineer view 
                            2: Admin Task Planning Mode
                            3: Admin Engineer Mode
                            4: Admin Task Production Mode");
            view = int.Parse(Console.ReadLine());
        }


    }


}