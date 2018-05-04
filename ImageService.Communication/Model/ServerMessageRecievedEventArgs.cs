using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Model
{
    class ServerMessageRecievedEventArgs : EventArgs
    {
        public ServerMessageTypeEnum Type { get; set; }
        public string Message { get; set; }
    }
}
