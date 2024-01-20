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
/// <param name="CreatedOn">The date and time when the Dependency was created (Requisite).</param>

public record Dependency
{
    public int Id; 
    public int DependentTask;
    public int DependentOnTask;
    string? CustomerEmail;
    string? Address;
    DateTime? CreatedOn;
    public bool IsActive = true;


    /// <summary>
    /// Initializes a new instance of the Dependency class.
    /// </summary>
    /// <param name="Id">Personal unique ID of the Dependency.</param>
    /// <param name="dependentTask">The ID of the dependent task.</param>
    /// <param name="dependentOnTask">The ID of the task on which it depends (Requisite).</param>
    /// <param name="customerEmail">The customer's email associated with the Dependency.</param>
    /// <param name="createdOn">The date and time when the Dependency was created (Requisite).</param>
    public Dependency(int Id, int dependentTask, int dependentOnTask, string? customerEmail, DateTime? createdOn)
    {
        this.Id = Id;
        DependentTask = dependentTask;
        DependentOnTask = dependentOnTask;
        CustomerEmail = customerEmail;
        CreatedOn = createdOn;
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
    CreatedOn={CreatedOn}, 
    ";
}
