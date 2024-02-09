﻿using DO;

namespace BO; 

/// <summary>
/// EngineerForTask class for the BO Layer
/// </summary>
public class Engineer
{
    public int Id { get; init; } 
    public string? Name { get; set; }
    public string? Email { get; set; }
    public ExperienceLevel? Level { get; set; }

    public double Cost { get; set; }
    public Task? Task { get; set; }

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

