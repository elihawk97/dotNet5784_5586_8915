namespace DO;

/// <summary>
/// Engineer Entity
/// </summary>
/// <param name="Id">Personal unique ID the engineer </param> 
/// <param name="Name"></param>
/// <param name="Email">Requisite</param>
/// <param name="Level"></param>
/// /// <param name="Cost"></param>
public record Engineer
{
    int Id;
    string? Name;
    string? Email;

    Enums.ExperienceLevel? EngineerExperience = null ;
    double Cost;

    // Default constructor with initialization
    public Engineer() : this(0, null, null, null, 0.0) { }


    // Parameterized constructor
    public Engineer(int id, string? name, string? email, Enums.ExperienceLevel? engineerExperience, double cost)
    {
        Id = id;
        Name = name;
        Email = email;
        EngineerExperience = engineerExperience;
        Cost = cost;
    }



}
