
using BO;
using System.Windows;
using Task;


namespace Engineer; 

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>
public partial class EngineerWindow : Window
{

    /// <summary>
    /// Static reference to the business logic layer.
    /// </summary>
    public static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    /// <summary>
    /// ID of the engineer being manipulated.
    /// </summary>
    private int EngineerId = 0;

    /// <summary>
    /// Gets or sets the selected experience level.
    /// </summary>
    public BO.Enums.ExperienceLevel ExpLevel { get; set; } = BO.Enums.ExperienceLevel.None;

    /// <summary>
    /// Dependency property for the current engineer being manipulated.
    /// </summary>
    public static readonly DependencyProperty CurrentEngineerProperty = DependencyProperty.Register(
        "CurrentEngineer",
        typeof(BO.Engineer),
        typeof(EngineerWindow),
        new PropertyMetadata(default(BO.Engineer)));

    /// <summary>
    /// Gets or sets the current engineer being manipulated.
    /// </summary>
    public BO.Engineer CurrentEngineer
    {
        get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
        set { SetValue(CurrentEngineerProperty, value); }
    }

   
    /// <summary>
    /// Handles the click event of the add/update button.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments.</param>
    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (EngineerId == 0)
            {
                // Add logic
                s_bl.Engineer.Create(CurrentEngineer);
            }
            else
            {
                // Update logic
                s_bl.Engineer.UpdateEngineer(EngineerId, CurrentEngineer);
            }

            // Close the window or navigate away
            this.Close();
        }
        catch (BlInvalidTaskCreation ex)
        {
         MessageBox.Show($"An error occurred: {ex.Message}", "Create Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}", "Update Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }



    /// <summary>
    /// Constructor for EngineerWindow.
    /// </summary>
    /// <param name="engineerId">The ID of the engineer being manipulated (optional).</param>
    /// <remarks>No explicit return value because it's a constructor.</remarks>n
    public EngineerWindow(int engineerId = 0)
    {
        InitializeComponent();
        EngineerId = engineerId;

        if (EngineerId == 0)
        {
            // Add mode: Assign a new object
            CurrentEngineer = new BO.Engineer();
        }
        else
        {
            // Update mode: Fetch the object from BL
            CurrentEngineer = s_bl.Engineer.ReadEngineer(EngineerId);
        }
    }
}
