using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ImageService.Communication.Model;

namespace ImageServiceWEB
{
    public class Log
    {
        #region Members
        public string Type { get; set; }
        public string Message { get; set; }
        #endregion

        public Log(LogMessage logMessage)
        {
            Message = logMessage.Message;
            if (logMessage.Type == ImageService.Infrastructure.Enums.LogMessageTypeEnum.INFO)
            {
                Type = "INFO";
            } else
            {
                if (logMessage.Type == ImageService.Infrastructure.Enums.LogMessageTypeEnum.FAIL)
                {
                    Type = "FAIL";
                } else
                {
                    Type = "WARNING";
                }
            }
        }
    }
}