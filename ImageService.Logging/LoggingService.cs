/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */
using ImageService.Logging.Modal;
using ImageService.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    public class LoggingService : ILoggingService
    {
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;

        public void Log(string message, LogMessageTypeEnum type)
        {
            MessageRecievedEventArgs log = new MessageRecievedEventArgs
            {
                Message = message,
                Status = type
            };
            MessageRecieved.Invoke(this, log);
        }
    }
}
