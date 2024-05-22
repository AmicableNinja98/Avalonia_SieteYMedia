using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace SieteYMedia.Helpers;

public class ImagenConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string s)
            return ImagenHelper.LoadFromResource(new Uri(s));
        else
            return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}