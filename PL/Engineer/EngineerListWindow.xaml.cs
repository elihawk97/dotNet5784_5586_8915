
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Engineer;

/// <summary>
/// Interaction logic for EngineerListWindow.xaml
/// </summary>
public partial class EngineerListWindow : Window
{

    /// <summary>
    /// Static reference to the business logic layer.
    /// </summary>
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    /// <summary>
    /// Gets or sets the selected experience level.
    /// </summary>
    public BO.Enums.ExperienceLevel ExpLevel { get; set; } = BO.Enums.ExperienceLevel.None;

    /// <summary>
    /// Gets or sets the list of engineers to be displayed.
    /// </summary>
    public IEnumerable<BO.Engineer> EngineerList
    {
        get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
        set { SetValue(EngineerListProperty, value); }
    }

    /// <summary>
    /// Dependency property for EngineerList.
    /// </summary>
    public static readonly DependencyProperty EngineerListProperty =
    DependencyProperty.Register("EngineerList",
    typeof(IEnumerable<BO.Engineer>),
    typeof(EngineerListWindow),
    new PropertyMetadata(null)
    );

    /// <summary>
    /// Refreshes the list of engineers displayed.
    /// </summary>
    private void RefreshEngineerList()
    {
        EngineerList = s_bl?.Engineer.ReadAll(null)!;
    }

    /// <summary>
    /// Handles the selection changed event of the engineer selector combo box.
    /// </summary>
    private void cbEngineerSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            // Create the filter based on the selected experience level
            Func<BO.Engineer, bool> filter = item => item.Level == ExpLevel;

            // Apply the filter
            EngineerList = (ExpLevel == BO.Enums.ExperienceLevel.None) ?
                        s_bl?.Engineer.ReadAll(null)! :
                        s_bl?.Engineer.ReadAll(filter)!;

        }

    /// <summary>
    /// Handles the selection changed event of the engineer selector combo box.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments.</param>
    
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        // Instantiate the EngineerWindow in "Add" mode (not passing an ID)
        EngineerWindow engineerWindow = new EngineerWindow(); // Assuming a parameterless constructor is "Add" mode
        engineerWindow.ShowDialog(); // ShowDialog to make it modal
        RefreshEngineerList(); 
    }

    /// <summary>
    /// Handles the mouse double click event of the list view.    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments.</param>
    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var listView = sender as ListView;
        var selectedEngineer = listView.SelectedItem as BO.Engineer;

        if (selectedEngineer != null)
        {
            // Assuming EngineerWindow has a constructor that takes an engineer's ID for update mode
            EngineerWindow engineerWindow = new EngineerWindow(selectedEngineer.Id);
            engineerWindow.ShowDialog(); // Show the window modally
        }
        RefreshEngineerList();
    }

    /// <summary>
    /// Constructor for EngineerListWindow.
    /// </summary>
    public EngineerListWindow()
    {
        EngineerList = s_bl?.Engineer.ReadAll(null)!;
        InitializeComponent();
    }
}
