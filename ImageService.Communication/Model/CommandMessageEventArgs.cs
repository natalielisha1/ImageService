﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Model
{
    public class CommandMessageEventArgs : EventArgs
    {
        public CommandMessage Message { get; set; }
    }
}
