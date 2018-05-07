using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ImageServiceGUI.Converters
{
    class TypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value)
            {
                case "INFO":
                    return System.Windows.Media.Colors.Green;
                case "WARNING":
                    return System.Windows.Media.Colors.Yellow;
                case "ERROR":
                    return System.Windows.Media.Colors.Red;
                default:
                    Console.WriteLine("Error in converting - value is unvalid"); //to change? exception?
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals(System.Windows.Media.Colors.Green))
            {
                return "INFO";
            } else
            {
                if (value.Equals(System.Windows.Media.Colors.Yellow))
                {
                    return "WARNING";
                } else
                {
                    if (value.Equals(System.Windows.Media.Colors.Red))
                    {
                        return "ERROR";
                    } else
                    {
                        Console.WriteLine("Error in converting back- value is unvalid"); //to change?
                        return "";
                    }
                }
            }
        }
    }
}
