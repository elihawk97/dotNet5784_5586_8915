using DO;

namespace BO; 

public class Engineer
{
    public int Id { get; init; } 
    public string? Name { get; set; }
    public string? Email { get; set; }
    public ExperienceLevel? Level { get; set; }

    public double Cost { get; set; }
    public Task? Task { get; set; }

}

