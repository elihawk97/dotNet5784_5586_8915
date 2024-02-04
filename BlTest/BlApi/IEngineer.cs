using DO;
using System.Data.Common;

namespace BlApi;

public interface IEngineer
{
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool> filter); 
    public BO.Engineer ReadEngineer(int Id);

    public void AddEngineer();

    public void DeleteEngineer();
    
    public void UpdateEngineer(BO.Engineer item);


}
