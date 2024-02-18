

using BL.BlApi;
using BL.BlImplementation;

namespace BlApi;

public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public IMilestone Milestone { get; }
    public ITools Tools { get; }

}
