/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex3
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Interfaces
{
    public interface IClientWrapper
    {
        /// <summary>
        /// The function reads the server's responses
        /// </summary>
        string Read();
        /// <summary>
        /// The function writes the client's requests
        /// </summary>
        /// <param name="message">the request</param>
        void Write(string message);
        /// <summary>
        /// The function closes all tools related to the client
        /// </summary>
        void Close();
    }
}
