using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Model
{
    public enum CommandMessageTypeEnum : int
    {
        LogRequest,
        LogAdded,
        ConfigRequest,
        ConfigData,
        ChangeConfig,
        AddHandler,
        RemoveHandler,
        Close
    }
}
