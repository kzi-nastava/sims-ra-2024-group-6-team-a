using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using static BookingApp.Resources.Enums;

namespace BookingApp.View.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is RequestStatus status)
                {
                    switch (status)
                    {
                        case RequestStatus.Accepted:
                            return Brushes.Green;
                        case RequestStatus.Onhold:
                            return Brushes.Orange;
                        case RequestStatus.Invalid:
                            return Brushes.Red;
                        default:
                            return DependencyProperty.UnsetValue; // Invalid value
                    }
                }
                return DependencyProperty.UnsetValue; // Value is not RequestStatus
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
}
