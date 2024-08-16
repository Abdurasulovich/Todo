using System.Globalization;

namespace To_Do.Services.Helpers;

public class CalendarImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is not null)
        {
            bool isDueSoon = (DateTime)value <= DateTime.Today;
            return isDueSoon is false ? "calendar.png" : "red_cal.png";
        }
        return "calendar.png";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
