using System.Globalization;

namespace To_Do.Services.Helpers;

internal class DateToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
     {
        try
        {
            if(value is not null)
            {
                bool isDueSoon = (DateTime)value <= DateTime.Today;

                return isDueSoon ? Color.Parse("#ef233c") : Color.Parse("#DCD7C9"); // or specify another default color
            }
            return Color.Parse("#DCD7C9");

        }catch (Exception ex)
        {
            return Color.Parse("#DCD7C9");
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
