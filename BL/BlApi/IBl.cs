

using BL.BlApi;
using BL.BlImplementation;

namespace BlApi;

/// <summary>
/// Interface for the Bl accessor to access the Bl level objects
/// </summary>
public interface IBl
{
    DateTime Clock { get; }
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public IMilestone Milestone { get; }
    public ITools Tools { get; }

    public void ClockForward();
    public void ClockBackward();
    public void Reset_Time();
}
