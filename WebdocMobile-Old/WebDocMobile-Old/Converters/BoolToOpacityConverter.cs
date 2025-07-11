#nullable enable
using System.Globalization;

namespace WebDocMobile.Converters
{
    public class BoolToOpacityConverter : IValueConverter
    {
        public double TrueOpacity { get; set; } = 1.0;
        public double FalseOpacity { get; set; } = 0.15;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isFilled)
            {
                return isFilled ? TrueOpacity : FalseOpacity;
            }
            return FalseOpacity;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
