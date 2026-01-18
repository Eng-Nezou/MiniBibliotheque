using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace MiniLibraryApp;

public class StringToImageSourceConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string url && !string.IsNullOrEmpty(url))
            return new UriImageSource { Uri = new Uri(url), CachingEnabled = true };
        return "book_placeholder.png"; // image locale par défaut
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
