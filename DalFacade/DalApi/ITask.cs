namespace DalApi;
using DO;
/// <summary>
/// Represents the interface for managing Task entities in the data access layer (DAL).
/// </summary>
public interface ITask : ICrud<Task>
{
    //int Create(Task item); // Creates new entity object in DAL
   // Task? Read(int id); // Reads entity object by its ID 
   // List<Task> ReadAll(); // stage 1 only, Reads all entity objects
   // void Update(Task item); // Updates entity object
   // void Delete(int id); // Deletes an object by its Id
}
