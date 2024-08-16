using System.Globalization;

namespace To_Do.Services.Helpers;

internal class DateTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return "Add due date";
        }

        if (value is DateTime dateTime)
        {
            return dateTime.ToString("ddd, MMM d, yyyy", culture);
        }

        return "Add due date";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
