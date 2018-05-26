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

namespace ImageServiceGUI.Converters
{
    class ConnectedToBrushConverter : IValueConverter
    {
        /// <summary>
        /// The function converts the boolean value of the Connected
        /// propety to the fitted brush color
        /// </summary>
        /// <param name="culture">unused</param>
        /// <param name="parameter">unused</param>
        /// <param name="targetType">unused</param>
        /// <param name="value">the boolean value of the Connected property
        /// , indicates whether the connection is on or off</param>
        /// <returns>brush color</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                //if the connection is on, return white
                case true:
                    return Brushes.White;
                //if the connection is off, return gray
                case false:
                    return Brushes.Gray;
                default:
                    Console.WriteLine("Error in converting - value is unvalid");
                    return null;
            }
        }

        /// <summary>
        /// The function converts the brush color
        /// tho the fitted boolean value
        /// </summary>
        /// <param name="culture">unused</param>
        /// <param name="parameter">unused</param>
        /// <param name="targetType">unused</param>
        /// <param name="value">unused</param>
        /// <returns>bool value</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
