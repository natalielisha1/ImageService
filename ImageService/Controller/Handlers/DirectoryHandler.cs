﻿using System;
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

        public DirectoryHandler(string path, IImageController controller, ILoggingService logging)
        {
            m_path = path;
            m_controller = controller;
            m_logging = logging;
            //TODO: Fill
        }

        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            //TODO: Check if that's correct
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
                    m_logging.Log(message, MessageTypeEnum.FAIL);
                }
            }
        }

        public void CloseHandler(string logMessage)
        {
            m_dirWatcher.EnableRaisingEvents = false;
            m_dirWatcher.Created -= OnFileCreated;
            m_dirWatcher.Dispose();
            DirectoryClose.Invoke(this, new DirectoryCloseEventArgs(m_path, logMessage));
        }

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
                    //Not yet..
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
