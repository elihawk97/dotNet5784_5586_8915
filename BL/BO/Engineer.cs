using DO;

namespace BO; 

/// <summary>
/// EngineerForTask class for the BO Layer
/// </summary>
public class Engineer
{
    public int Id { get; set; } 
    public string? Name { get; set; }
    public string? Email { get; set; }
    public Enums.ExperienceLevel? Level { get; set; }

    public double Cost { get; set; }
    public Task? Task { get; set; }

    /// <summary>
    /// Constructor that takes in parameters
    /// </summary>
    /// <param name="id">Engineer's ID</param>
    /// <param name="name">Engineer's name</param>
    /// <param name="email">Engineer's email</param>
    /// <param name="level">Engineer's experience leve</param>
    /// <param name="cost">Engineer's per hour salary</param>
    public Engineer(int id, string name, string email, Enums.ExperienceLevel? level, double cost)
    {
        Id = id;
        Name = name;
        Email = email;
        Level = level;
        Cost = cost;
    }

    /// <summary>
    /// Over riding the default ToString method to format the data
    /// </summary>
    /// <returns>formated object as a string</returns>
    public override string ToString() => $@"
    ID={Id}, 
    Name={Name}, 
    Email={Email}, 
    EngineerExperience={Level}, 
    Cost={Cost}
    Task={Task}
    ";

}

