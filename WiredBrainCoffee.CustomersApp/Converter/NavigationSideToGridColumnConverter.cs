using System.Globalization;
using System.Windows.Data;
using WiredBrainCoffee.CustomersApp.ViewModel;

namespace WiredBrainCoffee.CustomersApp.Converter;

public class NavigationSideToGridColumnConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var navigationSide = (NavigationSide)value;
        return navigationSide == NavigationSide.Left ? 0 : 2; // Value for Grid.Column
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}