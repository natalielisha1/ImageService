﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Model
{
    public enum ServerMessageTypeEnum : int
    {
        LogMessage,
        ConfigMessage,
        CloseMessage
    }
}
