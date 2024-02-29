
using System.Globalization;
using System.Windows.Data;

namespace PL;

/// <summary>
/// Converts an ID to content for display purposes.
/// </summary>
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
