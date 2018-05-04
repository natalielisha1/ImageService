using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Communication.Interfaces;
using System.Net.Sockets;
using System.IO;

namespace ImageService.Communication
{
    class ClientWrapper : IClientWrapper
    {
        private TcpClient client;
        private readonly object readLock = new object();
        private readonly object writeLock = new object();

        public ClientWrapper(TcpClient client)
        {
            this.client = client;
        }

        public string Read()
        {
            //Allow only one task to read at a time
            lock (readLock)
            {
                using (NetworkStream stream = client.GetStream())
                using (TextReader reader = new StreamReader(stream))
                {
                    return reader.ReadLine();
                }
            }
        }

        public void Write(string message)
        {
            //Allow only one task to write at a time
            lock (writeLock)
            {
                using (NetworkStream stream = client.GetStream())
                using (TextWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(message);
                }
            }
        }

        public void Close()
        {
            client.Close();
        }
    }
}
