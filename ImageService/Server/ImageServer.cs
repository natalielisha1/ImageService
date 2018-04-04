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

namespace ImageService.Server
{
    public class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;

        private Dictionary<int, ICommand> commands;
        #endregion

        #region Properties
        //The event that notifies about a new command being recieved
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;
        #endregion

        public ImageServer(ILoggingService logging)
        {
            m_logging = logging;
            IImageServiceModal modal = new ImageServiceModal();
            m_controller = new ImageController(modal);
        }

        public void StartServer()
        {
            string paths = System.Configuration.ConfigurationManager.AppSettings["Handler"];
            string[] pathArr = paths.Split(new char[] { ';' });
            foreach (string path in pathArr)
            {
                CreateHandler(path);
            }
        }

        public void CreateHandler(string dir)
        {
            IDirectoryHandler handler = new DirectoryHandler(dir, m_controller, m_logging);
            CommandRecieved += handler.OnCommandRecieved;
            handler.DirectoryClose += OnDirectoryClose;
            handler.StartHandleDirectory(dir);
        }

        private void OnDirectoryClose(object sender, DirectoryCloseEventArgs e)
        {
            if (sender is IDirectoryHandler)
            {
                IDirectoryHandler handler = (IDirectoryHandler) sender;
                CommandRecieved -= handler.OnCommandRecieved;
                handler.DirectoryClose -= OnDirectoryClose;
                m_logging.Log(e.DirectoryPath + @": " + e.Message, MessageTypeEnum.INFO);
            }
        }

        public void SendCommand()
        {
            //TODO: Maybe replace
            CommandRecieved.Invoke(this, new CommandRecievedEventArgs((int) CommandEnum.CloseCommand, new string[] { "Server close request" }, "*"));
        }
    }
}
