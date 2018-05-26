/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */

using ImageService.Communication.Interfaces;
using ImageService.Communication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using ImageService.Controller;
using System.IO;

namespace ImageService.Server
{
    public class ClientHandler : IClientHandler
    {
        #region Members
        private IImageController m_controller;
        #endregion

        /// <summary>
        /// Constructor for ClientHandler class
        /// </summary>
        /// <param name="controller">an image controller</param>
        public ClientHandler(IImageController controller)
        {
            m_controller = controller;
            //TODO: Maybe more stuff in here
        }

        public void HandleClient(IClientWrapper client)
        {
            Task task = new Task(() => {
                try
                {
                    while (true)
                    {
                        string message = client.Read();
                        if (message == null)
                        {
                            continue;
                        }
                        CommandMessage cmd = CommandMessage.FromJSONString(message);
                        bool result;
                        string newMessage = m_controller.ExecuteCommand((int)cmd.Type, cmd.Args, out result);
                        if (result)
                        {
                            client.Write(newMessage);
                        }
                    }
                } catch (IOException ex)
                {
                    client.Close();
                }
            });
            task.Start();
        }
    }
}
