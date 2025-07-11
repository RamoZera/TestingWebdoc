﻿#nullable enable
using System.Globalization;

namespace WebDocMobile.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public Color TrueColor { get; set; } = Colors.Transparent;
        public Color FalseColor { get; set; } = Colors.Transparent;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isFilled)
            {
                return isFilled ? TrueColor : FalseColor;
            }
            return FalseColor;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
