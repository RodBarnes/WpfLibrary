using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Common
{
    /// <summary>
    /// While using a ListBox with RadioButton Item style is the peferred means, if real
    /// radio buttons are desired, this converter supports using them in WPF.
    /// 
    /// Add a namepace attribute common:
    ///     xmlns:common="clr-namespace:Common;assembly=Common" 
    /// Add a Windows.Resource reference:
    ///     <common:RadioBoolToIntConverter x:Key="radioBoolToIntConverter" />
    /// Add the radio buttons to the XAML and set the IsChecked attribute:
    ///     IsChecked = "{Binding Path=<vm_int_property>, Converter={StaticResource radioBoolToIntConverter}, ConverterParameter=0}" Content="Option 1" ... />
    ///     IsChecked = "{Binding Path=<vm_int_property>, Converter={StaticResource radioBoolToIntConverter}, ConverterParameter=1}" Content="Option 2" ... />
    ///     IsChecked = "{Binding Path=<vm_int_property>, Converter={StaticResource radioBoolToIntConverter}, ConverterParameter=2}" Content="Option 3" ... />
    ///     ...
    /// </summary>
    public class RadioBoolToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value == int.Parse(parameter.ToString())) ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(false) ? DependencyProperty.UnsetValue : parameter;
        }
    }
}
