using System.Globalization;

namespace WebDocMobile.Converters
{
    /// <summary>
    /// Converts a boolean value to an opacity value.
    /// True -> Fully Opaque (1.0)
    /// False -> Mostly Transparent (0.15)
    /// </summary>
    public class BoolToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool isFilled && isFilled) ? 1.0 : 0.15;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}