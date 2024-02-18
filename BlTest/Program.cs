using DalApi;
using DO;
using BO;
using System.Globalization;

namespace BlTest;

internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();


    public static BO.Task taskInput()
    {
        Console.WriteLine("Enter data to create a new task:");

        int id;
        if (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("Invalid input for dependency ID. Please enter a valid integer.");
            return null;
        }

        string? nickName = Console.ReadLine();
        if (string.IsNullOrEmpty(nickName))
        {
            Console.WriteLine("Nickname cannot be empty. Please enter a valid name.");
            return null;
        }

        string? description = Console.ReadLine();
        if (string.IsNullOrEmpty(description))
        {
            Console.WriteLine("Description cannot be empty. Please enter a valid description.");
            return null;
        }

        bool isMilestone;
        if (!bool.TryParse(Console.ReadLine(), out isMilestone))
        {
            Console.WriteLine("Invalid input for milestone flag. Please enter True or False.");
            return null;
        }

        DateTime dateCreated;
        if (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy HH:mm:ss", null, DateTimeStyles.None, out dateCreated))
        {
            Console.WriteLine("Invalid date format. Please enter a valid date in MM/dd/yyyy HH:mm:ss format.");
            return null;
        }

        DateTime projectedStartDate;
        if (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy HH:mm:ss", null, DateTimeStyles.None, out projectedStartDate))
        {
            Console.WriteLine("Invalid date format. Please enter a valid date in MM/dd/yyyy HH:mm:ss format.");
            return null;
        }

        DateTime? actualStartTime = null;
        string actualStartTimeInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(actualStartTimeInput))
        {
            if (!DateTime.TryParseExact(actualStartTimeInput, "MM/dd/yyyy HH:mm:ss", null, DateTimeStyles.None, out actualStartTime))
            {
                Console.WriteLine("Invalid date format. Please enter a valid date in MM/dd/yyyy HH:mm:ss format.");
                return null;
            }
        }

        TimeSpan duration;
        if (!TimeSpan.TryParseExact(Console.ReadLine(), "hh\\:mm\\:ss", null, out duration))
        {
            Console.WriteLine("Invalid duration format. Please enter a valid duration in HH:mm:ss format.");
            return null;
        }

        DateTime deadline;
        if (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy HH:mm:ss", null, DateTimeStyles.None, out deadline))
        {
            Console.WriteLine("Invalid date format. Please enter a valid date in MM/dd/yyyy HH:mm:ss format.");
            return null;
        }

        DateTime? actualEndDate = null;
        string actualEndDateInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(actualEndDateInput))
        {
            if (!DateTime.TryParseExact(actualEndDateInput, "MM/dd/yyyy HH:mm:ss", null, DateTimeStyles.None, out actualEndDate))
            {
                Console.WriteLine("Invalid date format. Please enter a valid date in MM/dd/yyyy HH:mm:ss format.");
                return null;
            }
        }

        string deliverables = Console.ReadLine();

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
            engineerID,
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
        T newEntity = GetEntityInput<T>();

        try
        {
            if (typeof(T) == typeof(BO.Task))
            {
                s_bl.Task.Create(newEntity as BO.Task);
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
    }

    public static void ReadEntity<T>()
    {
        Console.WriteLine("Enter the id of the Task you wish to see");
        int id = int.Parse(Console.ReadLine());

        try
        {
            if (typeof(T) == typeof(BO.Task))
            {
                BO.Task? entityToRead = s_bl.Task.Read(id);
                Console.WriteLine(entityToRead);
            }
            else if (typeof(T) == typeof(BO.Engineer))
            {
                BO.Engineer? entityToRead = s_bl.Engineer.Read(id);
                Console.WriteLine(entityToRead);
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
    }

    public static void ReadAllEntities<T>()
    {
        try
        {
            IEnumerable<T?> itemList;

            if (typeof(T) == typeof(BO.Task))
            {
                itemList = s_bl.Task.ReadAll().Cast<T>();
            }
            else if (typeof(T) == typeof(BO.Engineer))
            {
                itemList = s_bl.Engineer.ReadAll().Cast<T>();
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
        catch (DalDoesNotExistException ex)
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
                s_bl.Task.Update(updateItem);
            }
            else if (typeof(T) == typeof(BO.Engineer))
            {
                BO.Engineer updateItem = GetEntityInput<BO.Engineer>();
                s_bl.Engineer.Update(updateItem);
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
    }

    public static void DeleteEntity<T>()
    {
        try
        {
            Console.WriteLine($"Enter the id of the {typeof(T).Name} you wish to delete");
            int id = int.Parse(Console.ReadLine());

            if (typeof(T) == typeof(BO.Task))
            {
                s_bl.Task.Delete(id);
            }
            else if (typeof(T) == typeof(BO.Engineer))
            {
                s_bl.Engineer.Delete(id);
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
    }
    public static void ResetEntities<T>()
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

        string taskInput = Console.ReadLine();
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


    public static void UseEntity<T>()
    {
        Console.WriteLine($@"Choose one of the following {typeof(T).Name}s to perform:
                        0: Exit sub-menu
                        1: Create {typeof(T).Name}
                        2: Read {typeof(T).Name}
                        3: Update {typeof(T).Name}
                        4: Delete {typeof(T).Name}
                        5: Read all {typeof(T).Name}s
                        6: Reset
                        Any Other number to go back
    ");


        string inputString = Console.ReadLine();
        int parsedInt;
        bool success = int.TryParse(inputString, out parsedInt);
        if (success)
        {
            switch (parsedInt)
            {
                case 1:
                    CreateEntity<T>();
                    break;
                case 2:
                    ReadEntity<T>();
                    break;
                case 3:
                    UpdateEntity<T>();
                    break;
                case 4:
                    DeleteEntity<T>();
                    break;
                case 5:
                    ReadAllEntities<T>();
                    break;
                case 6:
                    ResetEntities<T>();
                    break;
                default:
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Request Failed.");
        }
    }

    public static void ResetInitialData()
    {

        Console.Write("Do you really wish to Reset? (Y/N)"); //stage 3
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
        if (ans == "Y")
        {
            ResetEntities<BO.Task>();
            ResetEntities<Engineer>();
            ResetEntities<Dependency>();

            Initialization.Do();
        }
    }


    static void Main(string[] args)
    {
        Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
        if (ans == "Y") //stage 3
                        //Initialization.Do(s_bl); //stage 2
            Initialization.Do(); //stage 4

        int choice = 1;
        while (choice != 0)
        {

            Console.WriteLine("Please enter the project start date: ");
            string date = Console.ReadLine();
            DateTime? startDate = string.IsNullOrEmpty(date) ? (DateTime?)null : DateTime.Parse(date);

            s_bl.SetProjectStartDate(startDate);


            Console.WriteLine("Please enter the project start date: ");
            date = Console.ReadLine();
            DateTime? endDate = string.IsNullOrEmpty(date) ? (DateTime?)null : DateTime.Parse(date);

            s_bl.SetProjectStartDate(endDate);


            Console.WriteLine(@"These are the options of interfaces you may interact with:
                     0: Exit
                     1: Task
                     2: Engineer
                     4: Optional: Reset Initial Data
                    ");




            choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    UseEntity<BO.Task>();
                    break;
                case 2:
                    UseEntity<BO.Engineer>();
                    break;
                case 4:
                    ResetInitialData();
                    break;
                default:
                    choice = 0;
                    break;
            }
        }
    }


}