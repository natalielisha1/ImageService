/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */

using ImageService.Communication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Interfaces
{
    interface ITcpServer
    {
        void Start();
        void SendMessage(string message, ServerMessageTypeEnum type);
        void Stop();
    }
}
