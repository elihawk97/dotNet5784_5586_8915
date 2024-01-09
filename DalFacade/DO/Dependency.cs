namespace DO;


/// <summary>
/// Dependency Entity
/// </summary>
/// <param name="Id">Personal unique ID of the Dependency </param> 
/// <param name="DependentTask"></param>
/// <param name="DependentOnTask">Requisite</param>
/// <param name="CustomerEmail"></param>
/// /// <param name="Address"></param>
/// <param name="CreatedOn">Requisite</param>
/// <param name="Ship"></param>
/// /// <param name="Delivery"></param>

public record Dependency
{
    public int Id; 
    int DependentTask;
    int DependentOnTask;
    string? CustomerEmail;
    string? Address;
    DateTime? CreatedOn;
    DateTime? Ship;
    DateTime? Delivery;

    public Dependency(int Id, int dependentTask, int dependentOnTask, string? customerEmail, string? address, DateTime? createdOn, DateTime? ship, DateTime? delivery)
    {
        this.Id = Id;
        DependentTask = dependentTask;
        DependentOnTask = dependentOnTask;
        CustomerEmail = customerEmail;
        Address = address;
        CreatedOn = createdOn;
        Ship = ship;
        Delivery = delivery;
    }
}
