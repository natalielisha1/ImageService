﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Interfaces
{
    public interface IClientWrapper
    {
        string Read();
        void Write(string message);
        void Close();
    }
}
