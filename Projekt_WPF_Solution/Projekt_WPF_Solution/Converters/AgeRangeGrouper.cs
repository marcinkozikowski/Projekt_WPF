using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Projekt_WPF_Solution.Converters
{
    class AgeRangeGrouper : IValueConverter
    {
        public int GroupInterval { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int age = (int)value;
            if (age < GroupInterval)
            {
                return String.Format(culture, "Mniej niż {0}", GroupInterval);
            }
            else
            {
                int interval = (int)age / GroupInterval;
                int lowerLimit = interval * GroupInterval;
                int upperlimit = (interval + 1) * GroupInterval;
                return String.Format(culture, "{0} - {1}", lowerLimit, upperlimit);
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
