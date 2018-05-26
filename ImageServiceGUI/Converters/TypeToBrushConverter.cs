/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */
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
        /// <summary>
        /// The function converts an enum of a message
        /// type to it's fitted brush color
        /// </summary>
        /// <param name="culture">unused</param>
        /// <param name="parameter">unused</param>
        /// <param name="targetType">unused</param>
        /// <param name="value">type of message, enum</param>
        /// <returns>brush color</returns>
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

        /// <summary>
        /// The function converts the brush color
        /// to the fitted type of message
        /// </summary>
        /// <param name="culture">unused</param>
        /// <param name="parameter">unused</param>
        /// <param name="targetType">unused</param>
        /// <param name="value">unused</param>
        /// <returns>type of message, enum</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
