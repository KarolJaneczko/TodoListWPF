using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace TodoListWPF.Classes.Converters {
    public class DateOnlyConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is DateOnly dateOnly) {
                return dateOnly.ToDateTime(new TimeOnly(0));
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is DateTime dateTime) {
                return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
            }
            return DateOnly.MinValue;
        }
    }
}
