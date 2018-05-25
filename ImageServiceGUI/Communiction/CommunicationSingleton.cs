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
using ImageService.Communication.Model;
using ImageService.Communication;
using ImageService.Communication.Interfaces;
using System.IO;
using ImageService.Infrastructure.Enums;

namespace ImageServiceGUI.Communication
{
    public class CommunicationSingleton
    {
        #region Constants
        private const string DEFAULT_IP = "127.0.0.1";
        private const int DEFAULT_PORT = 5432;
        #endregion

        #region Singleton_Members
        private static volatile CommunicationSingleton instance;
        private static object mutex = new object();
        #endregion

        #region Properties
        public EventHandler<CommandMessageEventArgs> MessageArrived;
        #endregion

        #region Members
        private ITcpClient m_client;
        #endregion

        private CommunicationSingleton() {
            m_client = new TcpClientChannel();
            m_client.Connect(DEFAULT_IP, DEFAULT_PORT);
            Start();
        }

        public static CommunicationSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (mutex)
                    {
                        if (instance == null)
                        {
                            instance = new CommunicationSingleton();
                        }
                    }
                }
                return instance;
            }
        }

        private void Start()
        {
            Task task = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        string strMessage = m_client.Read();
                        MessageArrived?.Invoke(this, new CommandMessageEventArgs
                        {
                            Message = CommandMessage.FromJSONString(strMessage)
                        });
                    }
                    catch (IOException ex)
                    {
                        break;
                    }
                }
            });
            task.Start();
        }

        public void SendCommandToServer(CommandEnum commandID, string[] args,
                                        string message = null,
                                        bool status = true)
        {
            if (message == null)
            {
                message = "Command to server - " + commandID.ToString();
            }
            CommandMessage cmd = new CommandMessage
            {
                Status = status,
                Type = commandID,
                Message = message,
                Args = args
            };
            m_client.Write(cmd.ToJSONString());
        }
    }
}
