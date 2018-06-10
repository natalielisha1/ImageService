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
        #endregion

        public Log() { }

        public Log(LogMessage logMessage)
        {
            Message = logMessage.Message;
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