using DO;

namespace DalApi; 

public interface IDal
{
    ICrud<DO.Task> Task { get; }
    ICrud<DO.Engineer> Engineer { get; }
    ICrud<DO.Dependency> Dependency { get; }
    void SetProjectEndDate(DateTime? endDate);
    void SetProjectStartDate(DateTime? startDate);
    DateTime? getProjectStartDate();
    DateTime? getProjectEndDate(); 
}
