using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Modal.Event;

namespace ImageService.Controller.Handlers
{
    public class DirectoryHandler : IDirectoryHandler
    {
        #region Members
        // The image processing controller
        private IImageController m_controller;
        private ILoggingService m_logging;
        // The watcher of the dir
        private FileSystemWatcher m_dirWatcher;
        //The path of the directory
        private string m_path;
        #endregion

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;

        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            //TODO: Fill
            throw new NotImplementedException();
        }

        public void StartHandleDirectory(string dirPath)
        {
            //TODO: Fill
            throw new NotImplementedException();
        }
    }
}
