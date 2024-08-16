using System.Globalization;

namespace To_Do.Services.Helpers
{
    internal class DateToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is not null)
            {
                if (value is DateTime dueDate)
                {
                    return dueDate != null ? true : false;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
