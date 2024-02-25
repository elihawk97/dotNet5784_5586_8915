namespace BlImplementation;

using BL.BlApi;
using BL.BlImplementation;
using BlApi;
/// <summary>
/// Class that holds objects of the classes of functions to manipulate the 
/// Bl lists
/// </summary>
public class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();
    public IMilestone Milestone => new MilestoneImplementation();
    public ITools Tools => new ToolsImplementation();
}

