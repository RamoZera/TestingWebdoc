namespace WebDocMobile.Helpers.IndexToColorConverter
{
    internal class IndexToColorConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int index)
            {
                // Alternar entre branco e cinza
                return index % 2 == 0 ? Color.FromHex("#FFFFF") : Color.FromHex("#ff0000");
            }

            return Color.FromHex("#FFFFF"); // Cor padrão
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
