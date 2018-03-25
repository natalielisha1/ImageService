using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ImageService.Logging.Modal
{
    public enum MessageTypeEnum : int
    {
        INFO = EventLogEntryType.Information,
        WARNING = EventLogEntryType.Warning,
        FAIL = EventLogEntryType.Error
    }
}
