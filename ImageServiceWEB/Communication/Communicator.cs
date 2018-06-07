using ImageService.Communication;
using ImageService.Communication.Interfaces;
using ImageService.Communication.Model;
using ImageService.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ImageServiceWEB.Communication
{
    public class Communicator
    {
        #region Singleton
        public static Communicator Instance = new Communicator();
        #endregion

        #region Constants
        private const string DEFAULT_IP = "127.0.0.1";
        private const int DEFAULT_PORT = 5432;
        #endregion
        
        #region Members
        private ITcpClient m_client = null;
        private bool connected = false;
        #endregion

        #region Properties
        public EventHandler<CommandMessageEventArgs> MessageArrived;
        public bool Connected {
            get
            {
                if (!connected)
                {
                    if (m_client == null)
                    {
                        return false;
                    }
                    connected = m_client.Connected;
                }
                return connected;
            }
        }
        #endregion

        

        /// <summary>
        /// Constructs the communication singleton
        /// </summary>
        private Communicator()
        {
            m_client = new TcpClientChannel();
            connected = m_client.Connect(DEFAULT_IP, DEFAULT_PORT);
            Start();
        }

        /// <summary>
        /// The function starts the the communication
        /// between the GUI and the server
        /// </summary>
        private void Start()
        {
            if (!Connected)
            {
                return;
            }
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
                        connected = false;
                        break;
                    }
                }
            });
            task.Start();
        }

        /// <summary>
        /// The function sends commands (messages)
        /// from the WEB to the server
        /// </summary>
        public void SendCommandToServer(CommandEnum commandID, string[] args,
                                        string message = null,
                                        bool status = true)
        {
            if (!Connected)
            {
                return;
            }
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