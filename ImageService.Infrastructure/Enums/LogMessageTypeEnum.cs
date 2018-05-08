﻿/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ImageService.Infrastructure.Enums
{
    public enum LogMessageTypeEnum : int
    {
        INFO = EventLogEntryType.Information,
        WARNING = EventLogEntryType.Warning,
        FAIL = EventLogEntryType.Error
    }
}