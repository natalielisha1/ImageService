/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */
using ImageService.Communication.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication
{
    public class TcpClientChannel : ITcpClient
    {
        private IClientWrapper client;

        public bool Connected { get; private set; }

        public bool Connect(string ip, int port)
        {
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect(ep);
                client = new ClientWrapper(tcpClient);
                Connected = true;
                return true;
            } catch (Exception ex)
            {
                Connected = false;
                return false;
            }
        }

        public void Disconnect()
        {
            client.Close();
            Connected = false;
        }

        public string Read()
        {
            string read = client.Read();
            if (read == null)
            {
                Connected = false;
            }
            return read;
        }

        public void Write(string command)
        {
            client.Write(command);
        }
    }
}
