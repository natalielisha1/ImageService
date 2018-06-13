/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex3
 */
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

        //Returning an instance of HandlerManager
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

        //Setter for ILoggingService
        public ILoggingService Logging
        {
            set
            {
                m_logging = value;
            }
        }

        //Setter for IImageController
        public IImageController Controller
        {
            set
            {
                m_controller = value;
            }
        }

        /// <summary>
        /// The function creates a new handler with the given path
        /// and updates all related sources.
        /// </summary>
        /// <param name="path">The path of the handler we want to add</param>
        public void AddHandler(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }
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

        /// <summary>
        /// The function removes the handler responsible to the
        /// given path and updates all related sources
        /// </summary>
        /// <param name="path">The path of the handler we want to remove</param>
        public bool RemoveHandler(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }
            string oldPaths = ConfigurationManager.AppSettings["Handler"];
            if (oldPaths.Contains(path))
            {
                string newPaths = oldPaths.Replace(path, "");
                newPaths = newPaths.Replace(";;", ";");
                if (newPaths.EndsWith(";"))
                {
                    newPaths = newPaths.Substring(0, newPaths.Length - 1);
                }
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

        /// <summary>
        /// The function returns the current handlers
        /// </summary>
        /// <return>string array of the handlers</return>
        public string[] GetHandlers()
        {
            string paths = System.Configuration.ConfigurationManager.AppSettings["Handler"];
            return paths.Split(new char[] { ';' });
        }

        /// <summary>
        /// This function returns the first working handler
        /// </summary>
        /// <returns>a valid handler path (or null, if there isn't one)</returns>
        public string GetWorkingHandler()
        {
            string[] handlers = GetHandlers();
            foreach (string handler in handlers)
            {
                if (System.IO.Directory.Exists(handler))
                {
                    return handler;
                }
            }
            return null;
        }

        public bool SaveImageFile(string fileName, string b64Image)
        {
            string handler = GetWorkingHandler();
            if (string.IsNullOrWhiteSpace(handler))
            {
                return false;
            }
            try
            {
                byte[] imageBytes = Convert.FromBase64String(b64Image);
                string newImagePath = System.IO.Path.Combine(handler, fileName);
                System.IO.File.WriteAllBytes(newImagePath, imageBytes);
            } catch (Exception)
            {
                return false;
            }
            return true;
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

        /// <summary>
        /// The function invokes closing the server
        /// </summary>
        public void ServerClosing()
        {
            CommandRecieved?.Invoke(this, new CommandRecievedEventArgs((int)CommandEnum.CloseServer, new string[] { "Server close request" }, "*"));
        }
    }
}
