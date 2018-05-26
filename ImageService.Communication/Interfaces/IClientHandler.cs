/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Interfaces
{
    public interface IClientHandler
    {
        /// <summary>
        /// The function is responsible of handeling the client's requests
        /// </summary>
        /// <param name="client">an instance of a client, responsible of client's functions</param>
        void HandleClient(IClientWrapper client);
    }
}
