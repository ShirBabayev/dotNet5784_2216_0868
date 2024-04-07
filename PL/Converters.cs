using BO;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PL;

class ConvertIdToContent : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? "Add" : "Update";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

class ConvertDependencyToColor : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            TaskInList task = (TaskInList)values[0];
            IEnumerable<TaskInList> dependency = (IEnumerable<TaskInList>)values[1];

            if (task is null || dependency is null)
                return Brushes.Transparent;
            if (dependency.Any(t => t.Id == task.Id))
                return Brushes.LightGreen;
            return Brushes.Transparent;
        }
        catch { }
        return Brushes.Transparent; 
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

//public class InverseBooleanConverter : IValueConverter
//{
//    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//    {
//        if (value is bool boolValue)
//        {
//            return !boolValue;
//        }
//        return value;
//    }

//    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//    {
//        throw new NotImplementedException();
//    }
//}