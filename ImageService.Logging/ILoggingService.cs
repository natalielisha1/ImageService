/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    public interface ILoggingService
    {
        event EventHandler<MessageRecievedEventArgs> MessageRecieved;

        /// <summary>
        /// The Function is logging the given message
        /// </summary>
        /// <param name="message">The message we want to pass</param>
        /// <param name="type">The type of message (enum)</param>
        void Log(string message, MessageTypeEnum type);
    }
}
