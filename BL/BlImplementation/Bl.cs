
namespace BlImplementation;

using BL.BlApi;
using BL.BlImplementation;
using BlApi;

public class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();
    public IMilestone Milestone => new MilestoneImplementation();
    public ITools Tools => new ToolsImplementation();
}
