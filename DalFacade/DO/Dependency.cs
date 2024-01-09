using System.Diagnostics;
using System.Xml.Linq;

namespace DO;


/// <summary>
/// Represents the Dependency entity.
/// </summary>
/// <param name="Id">Personal unique ID of the Dependency.</param>
/// <param name="DependentTask">The ID of the dependent task.</param>
/// <param name="DependentOnTask">The ID of the task on which it depends (Requisite).</param>
/// <param name="CustomerEmail">The customer's email associated with the Dependency.</param>
/// <param name="Address">The address associated with the Dependency.</param>
/// <param name="CreatedOn">The date and time when the Dependency was created (Requisite).</param>
/// <param name="Ship">The date and time when the Dependency is scheduled to ship.</param>
/// <param name="Delivery">The date and time when the Dependency is scheduled for delivery.</param>

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

    /// <summary>
    /// Initializes a new instance of the Dependency class.
    /// </summary>
    /// <param name="Id">Personal unique ID of the Dependency.</param>
    /// <param name="dependentTask">The ID of the dependent task.</param>
    /// <param name="dependentOnTask">The ID of the task on which it depends (Requisite).</param>
    /// <param name="customerEmail">The customer's email associated with the Dependency.</param>
    /// <param name="address">The address associated with the Dependency.</param>
    /// <param name="createdOn">The date and time when the Dependency was created (Requisite).</param>
    /// <param name="ship">The date and time when the Dependency is scheduled to ship.</param>
    /// <param name="delivery">The date and time when the Dependency is scheduled for delivery.</param>
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

    /// <summary>
    /// Overrides the default ToString method to provide a formatted string representation of the Dependency object.
    /// </summary>
    /// <returns>A formatted string representing the Dependency object.</returns>
    public override string ToString() => $@"
    ID={Id}, 
    DependentTask={DependentTask}, 
    DependentOnTask={DependentOnTask}, 
    CustomerEmail={CustomerEmail}, 
    Address={Address}, 
    CreatedOn={CreatedOn}, 
    Ship={Ship}, 
    Delivery={Delivery}
    ";
}
