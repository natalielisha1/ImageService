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
using ImageService.Communication.Interfaces;
using ImageService.Communication.Model;
using System.Net.Sockets;
using System.Net;

namespace ImageService.Communication
{
    class TcpServerChannel : ITcpServer
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;
        private event EventHandler<ServerMessageRecievedEventArgs> MessageRecieved;

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
                        MessageRecieved += delegate (object sender, ServerMessageRecievedEventArgs e)
                        {
                            clientWrapper.Write(e.Message);
                            if (e.Type == ServerMessageTypeEnum.CloseMessage)
                            {
                                clientWrapper.Close();
                            }
                        };
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
