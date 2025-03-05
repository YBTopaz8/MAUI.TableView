using IValueConverter = Microsoft.UI.Xaml.Data.IValueConverter;
using Visibility = Microsoft.UI.Xaml.Visibility;

namespace YB.MauiDataGridView.Converters;

internal partial class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value is bool b && b ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
