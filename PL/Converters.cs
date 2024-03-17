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

    /// <summary>
    /// Determines the Color of the Text
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var str = value as string;
        if (str == null) return Brushes.Black;

        if (str.Contains("Created")) return Brushes.Blue;
        if (str.Contains("UnScheduled")) return Brushes.Blue;
        if (str.Contains("Scheduled")) return Brushes.Blue;
        if (str.Contains("OnTrack")) return Brushes.Yellow;
        if (str.Contains("InJeopardy")) return Brushes.Red;
        if (str.Contains("Done")) return Brushes.Green;



        if (str.Contains("Task")) return Brushes.White;

        return Brushes.White;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class ValueColorConverter : IValueConverter
{
    /// <summary>
    /// Determines the Color of the Cell
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        var str = value as string;
        if (str == null) return Brushes.White;



        if (str.Contains("Create") ||
            str.Contains("UnScheduled") || 
            str.Contains("Scheduled")) return Brushes.Blue;

        if (str.Contains("OnTrack")) return Brushes.Yellow;
        if (str.Contains("InJeopardy")) return Brushes.Red;
        if (str.Contains("Done")) return Brushes.Green;
        if (str.Contains("Task")) return Brushes.Black; 

        return Brushes.White; // Default color if parsing fails or value is not an int
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

