﻿namespace DO;

/// <summary>
/// Represents an enumeration of experience levels.
/// </summary>
public record Enums
{
    /// <summary>
    /// Enumerates different experience levels.
    /// </summary>
    public enum ExperienceLevel
    {
        Novice, 
        AdvancedBeginner, 
        Competent, 
        Proficient, 
        Expert

    }

    public enum CurrentStage
    {
        Planning, 
        Production
    }

    public enum CurrentUser
    {
        Admin, 
        Engineer
    }       

}
