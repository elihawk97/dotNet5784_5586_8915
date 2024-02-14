using DalApi;
using Dal;
using DO;
using System.Text.RegularExpressions;

namespace DalTest; 
internal class Program
{
    //static readonly IDal s_dal = new DalList(); //stage 2

    //static readonly IDal s_dal = new DalXml.DalXml();//stage 3
    static readonly IDal s_dal = Factory.Get; //stage 4

    public static DO.Task taskInput()
    {

        Console.WriteLine("Enter data to create new task");

        Console.WriteLine("Enter dependency ID:");
        int Id = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter task nickname: (String)");
        string? nickName = Console.ReadLine();

        Console.WriteLine("Enter task description: (String)");
        string? description = Console.ReadLine();

        Console.WriteLine("Is it a milestone? (True/False):");
        bool isMilestone = bool.Parse(Console.ReadLine());

        Console.WriteLine("Enter date created (MM/dd/yyyy HH:mm:ss):");
        DateTime dateCreated = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter projected start date (MM/dd/yyyy HH:mm:ss):");
        DateTime projectedStartDate = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter actual start time (optional, leave blank if not applicable - MM/dd/yyyy HH:mm:ss):");
        string actualStartTimeInput = Console.ReadLine();
        DateTime? actualStartTime = string.IsNullOrEmpty(actualStartTimeInput) ? (DateTime?)null : DateTime.Parse(actualStartTimeInput);

        Console.WriteLine("Enter task duration (HH:mm:ss):");
        TimeSpan duration = TimeSpan.Parse(Console.ReadLine());

        Console.WriteLine("Enter deadline (MM/dd/yyyy HH:mm:ss):");
        DateTime dealLine = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter actual end date (optional, leave blank if not applicable - MM/dd/yyyy HH:mm:ss):");
        string actualEndDateInput = Console.ReadLine();
        DateTime? actualEndDate = string.IsNullOrEmpty(actualEndDateInput) ? (DateTime?)null : DateTime.Parse(actualEndDateInput);

        Console.WriteLine("Enter deliverables: (String)");
        string deliverables = Console.ReadLine();

        Console.WriteLine("Enter additional notes: (String)");
        string notes = Console.ReadLine();

        Console.WriteLine("Enter engineer ID:" );
        int engineerID = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter experience level (Novice, AdvancedBeginner, Competent, Proficient, Expert):");
        DO.Enums.ExperienceLevel level = (DO.Enums.ExperienceLevel)Enum.Parse(typeof(DO.Enums.ExperienceLevel), Console.ReadLine());

        DO.Task newTask = new DO.Task(
            Id,
            nickName,
            description,
            dateCreated,
            projectedStartDate,
            actualStartTime,
            duration,
            dealLine,
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
        if (typeof(T) == typeof(DO.Task))
        {
            return (T)(object)taskInput();
        }
        else if (typeof(T) == typeof(DO.Engineer))
        {
            return (T)(object)EngineerInput();
        }
        else if (typeof(T) == typeof(DO.Dependency))
        {
            return (T)(object)DependencyInput();
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
            if (typeof(T) == typeof(DO.Task))
            {
                s_dal.Task.Create(newEntity as DO.Task);
            }
            else if (typeof(T) == typeof(DO.Engineer))
            {
                s_dal.Engineer.Create(newEntity as DO.Engineer);
            }
            else if (typeof(T) == typeof(DO.Dependency))
            {
                s_dal.Dependency.Create(newEntity as DO.Dependency);
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
            if (typeof(T) == typeof(DO.Task))
            {
                DO.Task? entityToRead = s_dal.Task.Read(id);
                Console.WriteLine(entityToRead);
            }
            else if (typeof(T) == typeof(DO.Engineer))
            {
                DO.Engineer? entityToRead = s_dal.Engineer.Read(id);
                Console.WriteLine(entityToRead);
            }
            else if (typeof(T) == typeof(DO.Dependency))
            {
                DO.Dependency? entityToRead = s_dal.Dependency.Read(id);
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

            if (typeof(T) == typeof(DO.Task))
            {
                itemList = s_dal.Task.ReadAll().Cast<T>();
            }
            else if (typeof(T) == typeof(DO.Engineer))
            {
                itemList = s_dal.Engineer.ReadAll().Cast<T>();
            }
            else if (typeof(T) == typeof(DO.Dependency))
            {
                itemList = s_dal.Dependency.ReadAll().Cast<T>();
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

            if (typeof(T) == typeof(DO.Task))
            {
                DO.Task updateItem = GetEntityInput<DO.Task>();
                s_dal.Task.Update(updateItem);
            }
            else if (typeof(T) == typeof(DO.Engineer))
            {
                DO.Engineer updateItem = GetEntityInput<DO.Engineer>();
                s_dal.Engineer.Update(updateItem);
            }
            else if (typeof(T) == typeof(DO.Dependency))
            {
                DO.Dependency updateItem = GetEntityInput<DO.Dependency>();
                s_dal.Dependency.Update(updateItem);
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

            if (typeof(T) == typeof(DO.Task))
            {
                s_dal.Task.Delete(id);
            }
            else if (typeof(T) == typeof(DO.Engineer))
            {
                s_dal.Engineer.Delete(id);
            }
            else if (typeof(T) == typeof(DO.Dependency))
            {
                s_dal.Dependency.Delete(id);
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
        if (typeof(T) == typeof(DO.Task))
        {
            s_dal.Task.Reset();
        }
        else if (typeof(T) == typeof(DO.Engineer))
        {
            s_dal.Engineer.Reset();
        }
        else if (typeof(T) == typeof(DO.Dependency))
        {
            s_dal.Dependency.Reset();
        }
        else
        {
            Console.WriteLine($"Unsupported entity type: {typeof(T).Name}");
        }
    }


    public static DO.Dependency DependencyInput()
    {
        Console.WriteLine("Enter data to create a new dependency:");
        Console.WriteLine("Enter dependency ID:");
        int Id = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter dependent task ID:");
        int dependentTask = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter dependent on task ID:");
        int dependentOnTask = int.Parse(Console.ReadLine());


        DO.Dependency newDependency = new DO.Dependency(
            Id,
            dependentTask,
            dependentOnTask
        );
        return newDependency;
    }

    public static DO.Engineer EngineerInput()
    {
        Console.WriteLine("Enter data to create a new engineer:");

        bool validInput = false;
        int id = 0;
        string name = null;
        string email = null;
        DO.Enums.ExperienceLevel? level = null;
        double cost = 0;
        string taskInput = null;
        DO.Task? task = null;

        while (!validInput)
        {
            validInput = true; // Assume success initially

            // Input: ID
            Console.WriteLine("Enter engineer ID:");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID format. Please enter a valid integer.");
                validInput = false;
            }

            // Input: Name
            Console.WriteLine("Enter engineer name:");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name cannot be empty. Please enter a valid name.");
                validInput = false;
            }

            // Input: Email
            Console.WriteLine("Enter engineer email:");
            email = Console.ReadLine();
            if (!IsValidEmail(email)) // Define IsValidEmail logic
            {
                Console.WriteLine("Invalid email format. Please enter a valid email address.");
                validInput = false;
            }

            // Input: Experience Level
            Console.WriteLine("Enter experience level (Novice, AdvancedBeginner, Competent, Proficient, Expert):");
            if (!Enum.TryParse<DO.Enums.ExperienceLevel>(Console.ReadLine(), out level))
            {
                Console.WriteLine("Invalid experience level. Please enter one of the listed options.");
                validInput = false;
            }

            // Input: Cost
            Console.WriteLine("Enter engineer cost:");
            if (!double.TryParse(Console.ReadLine(), out cost))
            {
                Console.WriteLine("Invalid cost format. Please enter a valid decimal number.");
                validInput = false;
            }

            // Don't initialize task at this point
            task = null;
        }

        DO.Engineer newEngineer = new DO.Engineer(
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
            ResetEntities<DO.Task>();
            ResetEntities<Engineer>();
            ResetEntities<Dependency>();

            Initialization.Do();
        }
    }

    private static bool IsValidEmail(string email)
    {
        // Basic checks with a regular expression (allows most common formats)
        const string emailPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

        // Additional checks (feel free to adjust based on your needs)
        if (string.IsNullOrEmpty(email))
        {
            return false; // Empty email
        }

        if (email.Length > 254)
        {
            return false; // Too long
        }

        if (!Regex.IsMatch(email, emailPattern))
        {
            return false; // Basic format check failed
        }

        // Consider adding checks for:
        // - Specific disallowed characters (e.g., consecutive dots, leading/trailing dots)
        // - TLD existence (using external services or databases)

        return true;
    }


    static void Main(string[] args)
    {
        Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
        if (ans == "Y") //stage 3
                        //Initialization.Do(s_dal); //stage 2
            Initialization.Do(); //stage 4

        int choice = 1;
        while (choice != 0)
        {

            Console.WriteLine("Please enter the project start date: ");
            string date = Console.ReadLine();
            DateTime? startDate = string.IsNullOrEmpty(date) ? (DateTime?)null : DateTime.Parse(date);

            s_dal.SetProjectStartDate(startDate);


            Console.WriteLine("Please enter the project start date: ");
            date = Console.ReadLine();
            DateTime? endDate = string.IsNullOrEmpty(date) ? (DateTime?)null : DateTime.Parse(date);

            s_dal.SetProjectStartDate(endDate);


            Console.WriteLine(@"These are the options of interfaces you may interact with:
                     0: Exit
                     1: Task
                     2: Engineer
                     3: Dependency
                     4: Optional: Reset Initial Data
                    ");


           

            choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    UseEntity<DO.Task>();
                    break;
                case 2:
                    UseEntity<DO.Engineer>();
                    break;
                case 3:
                    UseEntity<DO.Dependency>();
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


