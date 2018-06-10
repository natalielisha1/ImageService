/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex3
 */
using ImageService.Communication.Model;
using ImageService.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageServiceWEB.Communication;
using System.Threading;

namespace ImageServiceWEB.Models
{
    public class LogModel
    {
        #region Properties
        public List<LogMessage> Logs { get; set; }
        #endregion

        #region Members
        private static Communicator client = Communicator.Instance;
        private static object waitForUpdateLock = new object();
        #endregion

        /// <summary>
        /// Constructor for the LogModel class
        /// </summary>
        public LogModel()
        {
            Logs = new List<LogMessage>();
            client.MessageArrived += ProcessMessage;
            client.SendCommandToServer(CommandEnum.LogRequest, new string[] { });
            lock (waitForUpdateLock)
            {
                Monitor.Wait(waitForUpdateLock);
            }
        }

        /// <summary>
        /// The Function is processing the incoming messages from the server
        /// </summary>
        /// <param name="sender">The source of the message</param>
        /// <param name="e">The CommandMessageEventArgs arguments that were
        ///                 send along the message</param>
        public void ProcessMessage(object sender, CommandMessageEventArgs e)
        {
            //Extract the message from the EventArgs
            CommandMessage msg = e.Message;
            switch (msg.Type)
            {
                //Checking if the message is interesting to the Logs
                case CommandEnum.LogAdded:
                    foreach (LogMessage log in msg.LogMessages)
                    {
                        if (Logs.Count >= 2048)
                        {
                            Logs.RemoveAt(0);
                        }
                        Logs.Add(log);
                    }
                    lock (waitForUpdateLock)
                    {
                        Monitor.PulseAll(waitForUpdateLock);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
