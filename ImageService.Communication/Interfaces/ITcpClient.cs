/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Interfaces
{
    public interface ITcpClient
    {
        /// <summary>
        /// The function is responsible of
        /// connecting the client to the server
        /// </summary>
        /// <param name="ip">the TCP clien't ip address</param>
        /// <param name="port">the TCP client's port</param>
        bool Connect(string ip, int port);
        /// <summary>
        /// The function is responsible of
        /// writing requests to the server
        /// </summary>
        /// <param name="command"></param>
        void Write(string command);
        /// <summary>
        /// The function is responsible of reading the
        /// server's responses
        /// </summary>
        string Read();
        /// <summary>
        /// The function is responsible of
        /// disconnecting the client from the server
        /// </summary>
        void Disconnect();
    }
}
