using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace BookingApp.View.Converters
{
    public class CheckpointBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isLastReached && isLastReached)
            {
                if (parameter as string == "Background")
                {
                    return Brushes.AntiqueWhite;
                }
                else if (parameter as string == "BorderThickness")
                {
                    return new Thickness(2);
                }
                else if (parameter as string == "FontWeight")
                {
                    return FontWeights.Bold;
                }
            }

            if (parameter as string == "Background")
            {
                return Brushes.AliceBlue;
            }
            else if (parameter as string == "BorderThickness")
            {
                return new Thickness(1);
            }
            else if (parameter as string == "FontWeight")
            {
                return FontWeights.Normal;
            }

            return DependencyProperty.UnsetValue;
        }

                public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
