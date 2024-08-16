using System.Globalization;

namespace To_Do.Services.Helpers;

internal class LabelVisibilityConverter : IMultiValueConverter
{ 
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2)
            return false;

        var addNote = values[0] is string note && !string.IsNullOrEmpty(note);
        var dueDate = values[1] is DateTime date && date != null;

        return addNote && dueDate;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
