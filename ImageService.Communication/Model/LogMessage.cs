using ImageService.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Model
{
    public class LogMessage
    {
        public LogMessageTypeEnum Type { get; set; }
        public string Message { get; set; }
    }
}
