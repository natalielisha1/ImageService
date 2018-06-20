/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using ImageService.Communication.Model;
using ImageService.Infrastructure.Enums;
using ImageServiceWEB.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace ImageServiceWEB.Models
{
    /// <summary>
    /// Class ConfigModel.
    /// </summary>
    public class ConfigModel
    {
        #region Members
        /// <summary>
        /// The comm
        /// </summary>
        private static Communicator comm = Communicator.Instance;
        /// <summary>
        /// The wait for update lock
        /// </summary>
        private static object waitForUpdateLock = new object();
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the handlers.
        /// </summary>
        /// <value>The handlers.</value>
        public List<string> Handlers { get; set; }
        /// <summary>
        /// Gets or sets the output dir.
        /// </summary>
        /// <value>The output dir.</value>
        public string OutputDir { get; set; }
        /// <summary>
        /// Gets or sets the name of the source.
        /// </summary>
        /// <value>The name of the source.</value>
        public string SourceName { get; set; }
        /// <summary>
        /// Gets or sets the name of the log.
        /// </summary>
        /// <value>The name of the log.</value>
        public string LogName { get; set; }
        /// <summary>
        /// Gets or sets the size of the thumb.
        /// </summary>
        /// <value>The size of the thumb.</value>
        public string ThumbSize { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the ConfigModel class.
        /// </summary>
        public ConfigModel()
        {
            Handlers = new List<string>();
            comm.MessageArrived += ProcessMessage;
        }


        /// <summary>
        /// Processes the message.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The instance containing the event data, arguments</param>
        public void ProcessMessage(object sender, CommandMessageEventArgs e)
        {
            //Extract the message from the EventArgs
            CommandMessage msg = e.Message;
            switch (msg.Type)
            {
                //Checking if the message is interesting to the Settings
                case CommandEnum.AddHandler:
                    foreach (string handler in msg.Handlers)
                    {
                        Handlers.Add(handler);
                    }
                    break;
                case CommandEnum.RemoveHandler:
                    foreach (string handler in msg.Handlers)
                    {
                        Handlers.Remove(handler);
                    }
                    break;
                case CommandEnum.ConfigMessage:
                    OutputDir = msg.OutputDir;
                    SourceName = msg.LogSource;
                    LogName = msg.LogName;
                    ThumbSize = msg.ThumbSize.ToString();
                    Handlers = new List<string>();
                    foreach (string handler in msg.Handlers)
                    {
                        Handlers.Add(handler);
                    }
                    Handlers.RemoveAll(item => String.IsNullOrWhiteSpace(item));
                    lock (waitForUpdateLock)
                    {
                        Monitor.PulseAll(waitForUpdateLock);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Updates the configuration.
        /// </summary>
        public void UpdateConfig()
        {
            if (!comm.Connected)
            {
                OutputDir = "";
                SourceName = "";
                LogName = "";
                ThumbSize = "";
                Handlers = new List<string>();
                return;
            }
            comm.SendCommandToServer(CommandEnum.GetConfigCommand, new string[] { });
            lock (waitForUpdateLock)
            {
                Monitor.Wait(waitForUpdateLock);
            }
        }
    }
}