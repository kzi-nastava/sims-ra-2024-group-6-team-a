using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookingApp.Resources.Enums;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace BookingApp.View.Converters
{
    public class StatusAndDateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is RequestStatus status)
            {
                switch (status)
                {
                    case RequestStatus.Onhold:
                        _ = Brushes.Red;
                        return "OnHold";
                    case RequestStatus.Invalid:
                        return " / ";
                    case RequestStatus.Accepted:
                        if (values[1] is DateTime date)
                        {
                            return date.ToString(); // Format date as needed
                        }
                        break;
                }
            }
            return DependencyProperty.UnsetValue; // Invalid value
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
