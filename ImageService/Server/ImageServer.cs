using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Controller;
using ImageService.Logging;
using ImageService.Modal.Event;
using ImageService.Commands;

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
            //TODO: Fill
        }
    }
}
