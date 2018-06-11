﻿/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex3
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Infrastructure.Enums
{
    public enum CommandEnum : int
    {
        NewFileCommand,
        GetConfigCommand,
        CloseServer,
        LogRequest,
        LogAdded,
        ConfigMessage,
        ChangeConfig,
        AddHandler,
        RemoveHandler,
        RemoveImage,
        OK
    }
}
