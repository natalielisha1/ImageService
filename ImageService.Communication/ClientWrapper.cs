/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
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

        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;

        /// <summary>
        /// Constructor for the ClientWrapper class
        /// </summary>
        /// <param name="client">a Client instance,
        /// the client we want to activate</param>
        public ClientWrapper(TcpClient client)
        {
            this.client = client;
            this.stream = client.GetStream();
            this.reader = new StreamReader(this.stream);
            this.writer = new StreamWriter(this.stream);
            this.writer.AutoFlush = true;
        }

        public string Read()
        {
            try
            {
                //Allow only one task to read at a time
                lock (readLock)
                {
                    string message = reader.ReadLine();
                    if (message == null)
                    {
                        return null;
                    }
                    string lenStr = message.Split('\r')[0];
                    int len = int.Parse(lenStr);
                    char[] buffer = new char[len];
                    int readBytes = 0;
                    while (readBytes < len)
                    {
                        readBytes += reader.Read(buffer, readBytes, len - readBytes);
                    }
                    return new string(buffer);
                }
            }
            catch (Exception e)
            {
                //Maybe close the connection
                return null;
            }
        }

        public void Write(string message)
        {
            try
            {
                //Allow only one task to write at a time
                lock (writeLock)
                {
                    writer.WriteLine(message.Length);
                    writer.Write(message);
                }
            }
            catch (Exception e)
            {
                //Maybe close the connection
                return;
            }
        }

        public void Close()
        {
            lock (readLock)
            {
                lock (writeLock)
                {
                    writer.Dispose();
                    reader.Dispose();
                    stream.Dispose();
                    client.Close();
                }
            }
        }
    }
}
