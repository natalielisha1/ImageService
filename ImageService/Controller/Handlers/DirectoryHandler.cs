/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Modal.Event;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Infrastructure.Enums;

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

        private string[] m_fileExtensions = { ".jpg", ".png", ".gif", ".bmp" };
        #endregion

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;

        /// <summary>
        /// Constructor for Directory Handler class
        /// </summary>
        /// <param name="path">The path of the directory we would handle</param>
        /// <param name="controller">The controller that will be conected to this handler</param>
        /// <param name="logging">The looging service we will use for handling</param>
        public DirectoryHandler(string path, IImageController controller, ILoggingService logging)
        {
            m_path = path;
            m_controller = controller;
            m_logging = logging;
        }

        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            //check if it's close command, and if so close the handler
            if (e.CommandID == (int) CommandEnum.CloseCommand)
            {
                CloseHandler(e.Args[0]);
            }
        }

        public void StartHandleDirectory(string dirPath)
        {
            m_path = dirPath;
            m_dirWatcher = new FileSystemWatcher();
            m_dirWatcher.Path = m_path;
            m_dirWatcher.Created += OnFileCreated;
            m_dirWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// The function handles creating a new file
        /// </summary>
        /// <param name="sender">The sender of the command</param>
        /// <param name="e">The arguments that are given with the command</param>
        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            //Check file extension
            string filePath = e.FullPath;
            //Using "Any" to go through the entire array and check it against the Contains method of filePath
            if (m_fileExtensions.Any(filePath.Contains))
            {
                //Waiting for the file to be complete
                WaitForFileUnlock(filePath);
                bool result;
                string message = m_controller.ExecuteCommand((int) CommandEnum.NewFileCommand, new string[]{ filePath }, out result);
                if (result == false)
                {
                    m_logging.Log(message, LogMessageTypeEnum.FAIL);
                } else
                {
                    m_logging.Log(message, LogMessageTypeEnum.INFO);
                }
            }
        }

        /// <summary>
        /// The function closes the handler
        /// </summary>
        /// <param name="logMessage">the message that will be shown when closing the handler</param>
        public void CloseHandler(string logMessage)
        {
            m_dirWatcher.EnableRaisingEvents = false;
            m_dirWatcher.Created -= OnFileCreated;
            m_dirWatcher.Dispose();
            DirectoryClose.Invoke(this, new DirectoryCloseEventArgs(m_path, logMessage));
        }

        /// <summary>
        /// The function waits for the file to unlock
        /// </summary>
        /// <param name="file">the file we would like to wait for</param>
        public void WaitForFileUnlock(string file)
        {
            while (true)
            {
                FileStream fs = null;
                try
                {
                    fs = File.Open(file, FileMode.Open);
                }
                catch (IOException)
                {
                    //Nothing
                }
                if (fs != null)
                {
                    fs.Close();
                    return;
                }
            }
        }
    }
}
