using DO;
using System.Data.Common;

namespace BlApi;

/// <summary>
/// Class of function to manipulate the Engineer list in the Bl layer 
/// </summary>
public interface IEngineer
{
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool> filter); 
    public BO.Engineer? ReadEngineer(int id);

    public int Create(BO.Engineer item);

    public void DeleteEngineer(int id);
    
    public void UpdateEngineer(int id, BO.Engineer item);

    public void Reset();
}
