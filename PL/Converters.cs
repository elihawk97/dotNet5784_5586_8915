
using System.Globalization;
using System.Windows.Data;

namespace PL;

public class ConvertIdToContent : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter,
    CultureInfo culture)
    {


        return string.IsNullOrEmpty(value as string) ? "Add" : "Update";

    }
    public object ConvertBack(object value, Type targetType, object parameter,
    CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
