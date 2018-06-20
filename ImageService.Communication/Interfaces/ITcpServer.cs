/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using ImageService.Communication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Interfaces
{
    public interface ITcpServer
    {
        /// <summary>
        /// The function is responsible of starting the
        /// server's functuality
        /// </summary>
        void Start();
        /// <summary>
        /// The function is sending messages from the server
        /// </summary>
        /// <param name="message">the content of the message</param>
        /// <param name="type">enum that represents the type of message</param>
        void SendMessage(string message, ServerMessageTypeEnum type);
        /// <summary>
        /// The function stops the functuality of the server
        /// </summary>
        void Stop();
    }
}
