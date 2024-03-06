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
    private static DateTime s_Clock = DateTime.Now.Date;
    public DateTime Clock
    {
        get { return s_Clock; }
        private set { s_Clock = value; }
    }
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation(this);
    public IMilestone Milestone => new MilestoneImplementation();
    public ITools Tools => new ToolsImplementation();


    public void ClockForward() {
        Clock = Clock.AddDays(1);
    }
    public void ClockBackward()
    {
        Clock = Clock.AddDays(-1);
    }

    public void Reset_Time()
    {
        Clock = DateTime.Now;
    }

}

