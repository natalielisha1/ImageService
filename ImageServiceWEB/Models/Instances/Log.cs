/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ImageService.Communication.Model;
using ImageService.Infrastructure.Enums;

namespace ImageServiceWEB.Models.Instances
{
    public class Log
    {
        #region Members
        public string Type { get; set; }
        public string Message { get; set; }
        public string DoubleSlashedMessage { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the Log class.
        /// </summary>
        public Log() { }

        /// <summary>
        /// Initializes a new instance of the Log class.
        /// </summary>
        /// <param name="logMessage">The log message.</param>
        public Log(LogMessage logMessage)
        {
            Message = logMessage.Message;
            DoubleSlashedMessage = Message.Replace("\\", "\\\\");
            switch (logMessage.Type)
            {
                case LogMessageTypeEnum.INFO:   Type = "INFO";
                                                break;
                case LogMessageTypeEnum.FAIL:   Type = "FAIL";
                                                break;
                case LogMessageTypeEnum.WARNING:Type = "WARNING";
                                                break;
            }
        }
    }
}