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
    public class ConfigModel
    {
        #region Properties
        private static Communicator comm = Communicator.Instance;
        private static object waitForUpdateLock = new object();
        #endregion

        #region Members
        public List<string> Handlers { get; set; }
        public string OutputDir { get; set; }
        public string SourceName { get; set; }
        public string LogName { get; set; }
        public string ThumbSize { get; set; }
        #endregion

        public ConfigModel()
        {
            Handlers = new List<string>();
            comm.MessageArrived += ProcessMessage;
        }


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
                    lock (waitForUpdateLock)
                    {
                        Monitor.PulseAll(waitForUpdateLock);
                    }
                    break;
                default:
                    break;
            }
        }

        public void UpdateConfig()
        {
            comm.SendCommandToServer(CommandEnum.GetConfigCommand, new string[] { });
            lock (waitForUpdateLock)
            {
                Monitor.Wait(waitForUpdateLock);
            }
        }
    }
}