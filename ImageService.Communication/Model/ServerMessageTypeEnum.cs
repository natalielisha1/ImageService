﻿/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using System;
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
        CloseMessage,
        CloseHandlerMessage
    }
}
