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
using ImageService.Communication.Interfaces;
using ImageService.Communication.Model;
using System.Net.Sockets;
using System.Net;

namespace ImageService.Communication
{
    public class TcpServerChannel : ITcpServer
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;
        private event EventHandler<ServerMessageRecievedEventArgs> MessageRecieved;

        /// <summary>
        /// Constructor for TcpServerChannel
        /// </summary>
        /// <param name="port">the server's port</param>
        /// <param name="ch">the server's channel</param>
        public TcpServerChannel(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
        }

        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            listener.Start();
            Task task = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        IClientWrapper clientWrapper = new ClientWrapper(client);
                        EventHandler<ServerMessageRecievedEventArgs> newEventHandler = null;
                        newEventHandler = delegate (object sender, ServerMessageRecievedEventArgs e)
                        {
                            clientWrapper.Write(e.Message);
                            if (e.Type == ServerMessageTypeEnum.CloseMessage)
                            {
                                clientWrapper.Close();
                                MessageRecieved -= newEventHandler;
                            }
                        };
                        MessageRecieved += newEventHandler;
                        ch.HandleClient(clientWrapper);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
            });
            task.Start();

        }

        public void SendMessage(string message, ServerMessageTypeEnum type)
        {
            MessageRecieved?.Invoke(this, new ServerMessageRecievedEventArgs
            {
                Type = type,
                Message = message
            });
        }

        public void Stop()
        {
            listener.Stop();
        }
    }
}
