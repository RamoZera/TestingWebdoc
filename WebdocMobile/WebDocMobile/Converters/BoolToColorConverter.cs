using System.Globalization;

namespace WebDocMobile.Converters
{
    /// <summary>
    /// Converts a boolean value to a specific color.
    /// True -> Filled Color (Blue)
    /// False -> Empty Color (White)
    /// </summary>
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool isFilled && isFilled) ? Color.FromArgb("#0074C8") : Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}