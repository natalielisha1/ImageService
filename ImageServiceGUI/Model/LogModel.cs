﻿/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */
using ImageService.Communication.Model;
using ImageService.Infrastructure.Enums;
using ImageServiceGUI.Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    public class LogModel : IModel, INotifyPropertyChanged
    {
        #region Properties
        public ObservableCollection<LogMessage> Logs { get; set; }
        #endregion

        #region Members
        private CommunicationSingleton client;
        //static private int index = 0;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The function is responsible of notifying in case
        /// the property has changed
        /// </summary>
        /// <param name="prop">a property, as a string</param>
        public void NotifyPropertyChanged(string prop)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Constructor for the LogModel class
        /// </summary>
        public LogModel()
        {
            Logs = new ObservableCollection<LogMessage>();
            client = CommunicationSingleton.Instance;
            client.MessageArrived += ProcessMessage;
            client.SendCommandToServer(CommandEnum.LogRequest, new string[] { });
        }

        public void ProcessMessage(object sender, CommandMessageEventArgs e)
        {
            CommandMessage msg = e.Message;
            switch (msg.Type)
            {
                case CommandEnum.LogAdded:
                    foreach (LogMessage log in msg.LogMessages)
                    {
                        if (Logs.Count >= 2048)
                        {
                            //Logs.Remove(Logs[index]); //remove the oldest one in the collection
                            //index++;
                            Logs.RemoveAt(0);
                        }
                        Logs.Add(log);
                        NotifyPropertyChanged("Logs");
                    }
                    break;
                default:
                    break;
            }
        }

        public void SendMessage(string command, string[] args)
        {
            //empty, the log view doesn't request anything.
        }
    }
}
