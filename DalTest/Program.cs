using DalApi;
using Dal;
using DO;

namespace DalTest;
internal class Program
{
    //static readonly IDal s_dal = new DalList(); //stage 2

    //static readonly IDal s_dal = new DalXml.DalXml();//stage 3
    static readonly IDal s_dal = Factory.Get; //stage 4

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
 
        DateTime dateCreated = GetUserInput<DateTime>("Enter date created (MM/dd/yyyy HH:mm:ss):", input =>
        {
            bool isValid = DateTime.TryParse(input, out DateTime value)
                  && value >= DateTime.Today && value <= DateTime.Today; // Ensure date is not in the past
            return (isValid, value);
        });

        DateTime projectedStartDate = GetUserInput<DateTime>("Enter projected start date (MM/dd/yyyy HH:mm:ss):", input =>
        {
            bool isValid = DateTime.TryParse(input, out DateTime value)
                  && value >= DateTime.Today && value <= DateTime.Today; // Ensure date is not in the past
            return (isValid, value);
        });


        DateTime? actualStartTime = GetUserInput<DateTime>("Enter actual start time (optional, leave blank if not applicable - MM/dd/yyyy HH:mm:ss):", input =>
        {
            bool isValid = DateTime.TryParse(input, out DateTime value)
                  && value >= DateTime.Today && value <= DateTime.Today; // Ensure date is not in the past
            return (isValid, value);
        });


        TimeSpan duration = GetUserInput<TimeSpan>("Enter task duration (HH:mm:ss):", input =>
        {
            bool isValid = TimeSpan.TryParse(input, out TimeSpan value)
                && value >= TimeSpan.Zero; // Ensure non-negative time span
            return (isValid, value);
        });

        DateTime dealLine = GetUserInput<DateTime>("Enter deadline (MM/dd/yyyy HH:mm:ss):", input =>
        {
            bool isValid = DateTime.TryParse(input, out DateTime value)
                  && value >= DateTime.Today && value <= DateTime.Today; // Ensure date is not in the past
            return (isValid, value);
        });


        DateTime? actualEndDate = GetUserInput<DateTime>("Enter actual end date (optional, leave blank if not applicable - MM/dd/yyyy HH:mm:ss):", input =>
        {
            bool isValid = DateTime.TryParse(input, out DateTime value)
                  && value >= DateTime.Today && value <= DateTime.Today; // Ensure date is not in the past
            return (isValid, value);
        });

        string deliverables = GetUserInput<string>("Enter deliverables: (String)", input =>
        {
            bool isValid = !string.IsNullOrEmpty(input);
            return (isValid, input); // input is directly the value for string types
        });

        string notes = GetUserInput<string>("Enter additional notes: (String)", input =>
        {
            bool isValid = !string.IsNullOrEmpty(input);
            return (isValid, input); // input is directly the value for string types
        });

        Console.WriteLine("Enter engineer ID:");
        int engineerID = int.Parse(Console.ReadLine());

        // For Enums (e.g., level)
        DO.Enums.ExperienceLevel level = GetUserInput<DO.Enums.ExperienceLevel>("Enter experience level (e.g., Novice, AdvancedBeginner):", input =>
        {
            bool isValid = Enum.TryParse<DO.Enums.ExperienceLevel>(input, out var value);
            return (isValid, value);
        });

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

    public static void ActivateEntity<T>()
    {
        try
        {
            Console.WriteLine($"Enter the id of the {typeof(T).Name} you wish to delete");
            int id = int.Parse(Console.ReadLine());

            if (typeof(T) == typeof(DO.Task))
            {
                s_dal.Task.Activate(id);
            }
            else if (typeof(T) == typeof(DO.Engineer))
            {
                s_dal.Engineer.Activate(id);
            }
            else if (typeof(T) == typeof(DO.Dependency))
            {
                s_dal.Dependency.Activate(id);
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
        Console.WriteLine("Enter engineer ID:");
        int Id = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter engineer name:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter engineer email:");
        string email = Console.ReadLine();

        Console.WriteLine("Enter engineer cost:");
        double cost = double.Parse(Console.ReadLine());

        Console.WriteLine("Enter experience level (Novice, AdvancedBeginner, Competent, Proficient, Expert):");
        DO.Enums.ExperienceLevel level = (DO.Enums.ExperienceLevel)Enum.Parse(typeof(DO.Enums.ExperienceLevel), Console.ReadLine());

        DO.Engineer newEngineer = new DO.Engineer(
            Id,
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
                        7: Activate Entity
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
                case 7:
                    ActivateEntity<T>();
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

    public static void SetProjectDates()
    {

        try
        {
            Console.WriteLine("Please enter the project start date: ");
            string? date = Console.ReadLine();

            DateTime startDate;

            if (date != null)
            {
                startDate = DateTime.Parse(date);
                s_dal.SetProjectStartDate(startDate);
            }


            Console.WriteLine("Please enter the project end date: ");
            date = Console.ReadLine();


            DateTime endDate;

            if (date != null)
            {
                endDate = DateTime.Parse(date);
                s_dal.SetProjectEndDate(endDate);
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }


    static void Main(string[] args)
    {

        SetProjectDates();

        Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
        if (ans == "Y") //stage 3
                        //Initialization.Do(s_dal); //stage 2
            Initialization.Do(); //stage 4

        int choice = 1;
        while (choice != 0)
        {
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

            Console.WriteLine(s_dal.getProjectStartDate().ToString());
            Console.WriteLine(s_dal.getProjectEndDate().ToString());

        }
    }


}