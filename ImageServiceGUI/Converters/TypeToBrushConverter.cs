using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using ImageService.Infrastructure.Enums;

namespace ImageServiceGUI.Converters
{
    class TypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case LogMessageTypeEnum.INFO:
                    return Brushes.Green;
                case LogMessageTypeEnum.WARNING:
                    return Brushes.Yellow;
                case LogMessageTypeEnum.FAIL:
                    return Brushes.Red;
                default:
                    Console.WriteLine("Error in converting - value is unvalid");
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
