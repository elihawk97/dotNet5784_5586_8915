using System.Windows;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Controls; // Add this for DataGridCell

/// <summary>
/// Converts an ID to content for display purposes.
/// </summary>
namespace PL;


public class ConvertIdToContent : IValueConverter
{

    /// <summary>
    /// Converts an ID value to content based on certain conditions.
    /// </summary>
    /// <param name="value">The ID value to be converted.</param>
    /// <param name="targetType">The type of the target property.</param>
    /// <param name="parameter">Optional parameter.</param>
    /// <param name="culture">The culture information to use in the conversion.</param>
    /// <returns>The converted content.</returns>
    public object Convert(object value, Type targetType, object parameter,
    CultureInfo culture)
    {


        if (value is int id)
        {
            return id == 0 ? "Add" : "Update";
        }
        // Adjust or remove the string logic as necessary
        return string.IsNullOrEmpty(value as string) ? "Add" : "Update";

    }

    /// <summary>
    /// Converts back a value, which is not implemented in this converter.
    /// </summary>
    /// <param name="value">The value to convert back.</param>
    /// <param name="targetType">The type to convert back to.</param>
    /// <param name="parameter">Optional parameter.</param>
    /// <param name="culture">The culture information to use in the conversion.</param>
    /// <returns>Throws a NotImplementedException.</returns>
    public object ConvertBack(object value, Type targetType, object parameter,
    CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class ForeGroundConvertor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var str = value as string;
        if (str == null) return Brushes.Black;

        if (str == "Created") return Brushes.Blue;
        if (str == "UnScheduled") return Brushes.Yellow;
        if (str == "Scheduled") return Brushes.Purple;
        if (str == "OnTrack") return Brushes.Pink;
        if (str == "InJeopardy") return Brushes.Red;
        if (str == "Done") return Brushes.Green;

        return Brushes.Black;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class ValueColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var str = value as string;
        if (str == null) return Brushes.White;

        if (str == "Created") return Brushes.Blue;
        if (str == "UnScheduled") return Brushes.Yellow;
        if (str == "Scheduled") return Brushes.Purple;
        if (str == "OnTrack") return Brushes.Pink;
        if (str == "InJeopardy") return Brushes.Red;
        if (str == "Done") return Brushes.Green;

        return Brushes.White; // Default color if parsing fails or value is not an int
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

