
using System.Globalization;
using System.Windows.Data;

namespace PL;

public class ConvertIdToContent : IValueConverter
{
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
    public object ConvertBack(object value, Type targetType, object parameter,
    CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
