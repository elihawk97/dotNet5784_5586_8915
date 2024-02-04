using BlApi;

namespace BlImplementation; 

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

}
