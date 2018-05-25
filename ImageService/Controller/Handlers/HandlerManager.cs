using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal.Event;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller.Handlers
{
    public class HandlerManager
    {
        #region Singleton_Members
        private static volatile HandlerManager instance;
        private static object mutex = new object();
        #endregion

        #region Members
        private ILoggingService m_logging = null;
        private IImageController m_controller = null;
        #endregion

        #region Properties
        //The event that notifies about a new command being recieved
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;
        #endregion

        private HandlerManager() { }

        public static HandlerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (mutex)
                    {
                        if (instance == null)
                        {
                            instance = new HandlerManager();
                        }
                    }
                }
                return instance;
            }
        }

        public ILoggingService Logging
        {
            set
            {
                m_logging = value;
            }
        }

        public IImageController Controller
        {
            set
            {
                m_controller = value;
            }
        }

        public void AddHandler(string path)
        {
            string oldPaths = ConfigurationManager.AppSettings["Handler"];
            if (!oldPaths.Contains(path))
            {
                string newPaths = oldPaths + ";" + path;
                Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
                config.AppSettings.Settings["Handler"].Value = newPaths;
                config.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
            CreateHandler(path);
        }

        public bool RemoveHandler(string path)
        {
            string oldPaths = ConfigurationManager.AppSettings["Handler"];
            if (oldPaths.Contains(path))
            {
                string newPaths = oldPaths.Replace(path, "");
                newPaths = newPaths.Replace(";;", ";");
                Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
                config.AppSettings.Settings["Handler"].Value = newPaths;
                config.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
            CommandRecieved?.Invoke(this, new CommandRecievedEventArgs((int) CommandEnum.RemoveHandler,
                                                                       new string[] { "Recieved remove handler request for " + path },
                                                                       path));
            return true;
        }

        public string[] GetHandlers()
        {
            string paths = System.Configuration.ConfigurationManager.AppSettings["Handler"];
            return paths.Split(new char[] { ';' });
        }

        /// <summary>
        /// The Function creates a handler
        /// </summary>
        /// <param name="dir">The directory of the file we want to handle</param>
        private void CreateHandler(string dir)
        {
            if (System.IO.Directory.Exists(dir))
            {
                IDirectoryHandler handler = new DirectoryHandler(dir, m_controller, m_logging);
                CommandRecieved += handler.OnCommandRecieved;
                handler.DirectoryClose += OnDirectoryClose;
                handler.StartHandleDirectory(dir);
                m_logging.Log("Created and started handler for: " + dir, LogMessageTypeEnum.INFO);
            }
            else
            {
                m_logging.Log("Failed to create handler for: " + dir, LogMessageTypeEnum.FAIL);
            }
        }

        /// <summary>
        /// The Function closes the handler of a file
        /// </summary>
        /// <param name="sender">The sender of the close command</param>
        /// <param name="e">The arguments that came with the command (close directory)</param>
        private void OnDirectoryClose(object sender, DirectoryCloseEventArgs e)
        {
            if (sender is IDirectoryHandler)
            {
                IDirectoryHandler handler = (IDirectoryHandler)sender;
                CommandRecieved -= handler.OnCommandRecieved;
                handler.DirectoryClose -= OnDirectoryClose;
                m_logging.Log(e.DirectoryPath + @": " + e.Message, LogMessageTypeEnum.INFO);
            }
        }
    }
}
