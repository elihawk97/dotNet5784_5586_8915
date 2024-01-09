namespace DO;

/// <summary>
/// Represents the Engineer entity.
/// </summary>
/// <param name="Id">Personal unique ID of the engineer.</param>
/// <param name="Name">The name of the engineer.</param>
/// <param name="Email">The email (Requisite) associated with the engineer.</param>
/// <param name="EngineerExperience">The experience level of the engineer.</param>
/// <param name="Cost">The cost associated with the engineer.</param>
public record Engineer
{
    public int Id;
    string? Name;
    string? Email;

    Enums.ExperienceLevel? EngineerExperience;
    double Cost;

    /// <summary>
    /// Initializes a new instance of the Engineer class with default values.
    /// </summary>    public Engineer() : this(0, null, null, null, 0.0) { }
    public Engineer(int id, string? name, string? email, Enums.ExperienceLevel? engineerExperience, double cost)
    {
        Id = id;
        Name = name;
        Email = email;
        EngineerExperience = engineerExperience;
        Cost = cost;
    }

    /// <summary>
    /// Initializes a new instance of the Engineer class with specified values.
    /// </summary>
    /// <param name="id">Personal unique ID of the engineer.</param>
    /// <param name="name">The name of the engineer.</param>
    /// <param name="email">The email (Requisite) associated with the engineer.</param>
    /// <param name="engineerExperience">The experience level of the engineer.</param>
    /// <param name="cost">The cost associated with the engineer.</param>
    public override string ToString() => $@"
    ID={Id}, 
    Name={Name}, 
    Email={Email}, 
    EngineerExperience={EngineerExperience}, 
    Cost={Cost}
    ";

}
