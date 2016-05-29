using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Projekt_WPF_Solution.Converters
{
    class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool)
            {
                bool val = (bool)value;
                return val == true ? 0 : 1;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is int)
            {
                int index = (int)value;
                if(index == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return value;
        }
    }
}
