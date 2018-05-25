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
using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Modal.Event;
using ImageService.Modal;
using ImageService.Commands;
using ImageService.Infrastructure.Enums;
using ImageService.Communication.Interfaces;
using ImageService.Communication;
using ImageService.Communication.Model;

namespace ImageService.Server
{
    public class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        private IClientHandler m_clientHandler;
        private ITcpServer m_TCPServer;
        private HandlerManager m_handlerManager;
        private LogStorage m_logStorage;

        private Dictionary<int, ICommand> commands;
        #endregion

        #region Properties
        //The event that notifies about a new command being recieved
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;
        #endregion

        /// <summary>
        /// Constructor for ImageServer class
        /// </summary>
        /// <param name="logging">the logging service that will be connected to the image service</param>
        public ImageServer(ILoggingService logging, int port)
        {
            //while (!System.Diagnostics.Debugger.IsAttached) { }
            m_logging = logging;
            IImageServiceModal modal = new ImageServiceModal();
            m_controller = new ImageController(modal);
            m_clientHandler = new ClientHandler(m_controller);
            m_TCPServer = new TcpServerChannel(port, m_clientHandler);
            m_handlerManager = HandlerManager.Instance;
            m_handlerManager.Logging = m_logging;
            m_handlerManager.Controller = m_controller;
            m_handlerManager.CommandRecieved += delegate (object sender, CommandRecievedEventArgs e)
            {
                switch (e.CommandID)
                {
                    case (int)CommandEnum.RemoveHandler:
                        CommandMessage message = new CommandMessage
                        {
                            Status = true,
                            Type = CommandEnum.RemoveHandler,
                            Message = @"Removed handler " + e.RequestDirPath,
                            Handlers = new string[] { e.RequestDirPath }
                        };
                        m_logging.Log(message.Message, LogMessageTypeEnum.INFO);
                        m_TCPServer.SendMessage(message.ToJSONString(), ServerMessageTypeEnum.CloseHandlerMessage);
                        break;
                    default:
                        break;
                }
            };
            m_logStorage = LogStorage.Instance;
            m_logging.MessageRecieved += m_logStorage.AddLog;
        }

        /// <summary>
        /// The Function starts the service
        /// </summary>
        public void StartServer()
        {
            m_TCPServer.Start();

            m_logging.MessageRecieved += delegate (object sender, MessageRecievedEventArgs e)
            {
                LogMessage newLogMessage = new LogMessage
                {
                    Type = e.Status,
                    Message = e.Message
                };

                List<LogMessage> newLogList = new List<LogMessage>();
                newLogList.Add(newLogMessage);

                CommandMessage msg = new CommandMessage
                {
                    Status = true,
                    Type = CommandEnum.LogAdded,
                    Message = @"A new log entry was made",
                    LogMessages = newLogList
                };

                m_TCPServer.SendMessage(msg.ToJSONString(), ServerMessageTypeEnum.LogMessage);
            };

            string paths = System.Configuration.ConfigurationManager.AppSettings["Handler"];
            string[] pathArr = paths.Split(new char[] { ';' });
            foreach (string path in pathArr)
            {
                m_handlerManager.AddHandler(path);
                //CreateHandler(path);
            }
        }

        //TODO: Remove
        /*
        /// <summary>
        /// The Function creates a handler
        /// </summary>
        /// <param name="dir">The directory of the file we want to handle</param>
        public void CreateHandler(string dir)
        {
            if (System.IO.Directory.Exists(dir))
            {
                IDirectoryHandler handler = new DirectoryHandler(dir, m_controller, m_logging);
                CommandRecieved += handler.OnCommandRecieved;
                handler.DirectoryClose += OnDirectoryClose;
                handler.StartHandleDirectory(dir);
                m_logging.Log("Created and started handler for: " + dir, LogMessageTypeEnum.INFO);
            } else
            {
                m_logging.Log("Failed to create handler for: " + dir, LogMessageTypeEnum.FAIL);
            }
        }*/

        //TODO: Remove
        /*
        /// <summary>
        /// The Function closes the handler of a file
        /// </summary>
        /// <param name="sender">The sender of the close command</param>
        /// <param name="e">The arguments that came with the command (close directory)</param>
        private void OnDirectoryClose(object sender, DirectoryCloseEventArgs e)
        {
            if (sender is IDirectoryHandler)
            {
                IDirectoryHandler handler = (IDirectoryHandler) sender;
                CommandRecieved -= handler.OnCommandRecieved;
                handler.DirectoryClose -= OnDirectoryClose;
                m_logging.Log(e.DirectoryPath + @": " + e.Message, LogMessageTypeEnum.INFO);
            }
        }
        */

        /// <summary>
        /// The Function sends CLOSE command (requesting to close the server)
        /// </summary>
        public void SendCommand()
        {
            CommandRecieved?.Invoke(this, new CommandRecievedEventArgs((int) CommandEnum.CloseServer, new string[] { "Server close request" }, "*"));
            m_TCPServer.Stop();
        }
    }
}
