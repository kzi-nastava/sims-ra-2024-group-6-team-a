using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace BookingApp.View.Converters
{
    public class RadioButtonToIntConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            /*var para = System.Convert.ToInt32(parameter.ToString());
            var myChoice = System.Convert.ToInt32(value);
            return para == myChoice;*/
            int integer = (int)value;
            if (integer == int.Parse(parameter.ToString()))
                return true;
            else
                return false;
        }

        //Convert the radiobutton to a number by checking isChecked, if true return the parameter as the convertion result, if false say to DoNothing
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return parameter;
        }
    }
}
