

using BL.BlApi;
using BL.BlImplementation;

namespace BlApi;

/// <summary>
/// Interface for the Bl accessor to access the Bl level objects
/// </summary>
public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public IMilestone Milestone { get; }
    public ITools Tools { get; }

}
