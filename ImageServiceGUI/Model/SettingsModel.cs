/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Communication.Model;
using ImageService.Infrastructure.Enums;
using ImageServiceGUI.Communication;

namespace ImageServiceGUI.Model
{
    public class SettingsModel : IModel, INotifyPropertyChanged
    {
        #region Properties
        private CommunicationSingleton client;
        private string outputDir;
        private string sourceName;
        private string logName;
        private string thumSize;
        #endregion

        #region Members
        public ObservableCollection<string> Handlers { get; set; }
        public string OutputDir
        {
            get { return outputDir; }
            set
            {
                outputDir = value;
                NotifyPropertyChanged("OutputDir");
            }
        }
        public string SourceName
        {
            get { return sourceName; }
            set
            {
                sourceName = value;
                NotifyPropertyChanged("SourceName");
            }
        }
        public string LogName
        {
            get { return logName; }
            set
            {
                logName = value;
                NotifyPropertyChanged("LogName");
            }
        }
        public string ThumSize
        {
            get { return thumSize; }
            set
            {
                thumSize = value;
                NotifyPropertyChanged("ThumSize");
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

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
        /// Constructor for SettingsModel class
        /// </summary>
        public SettingsModel()
        {
            Handlers = new ObservableCollection<string>();
            client = CommunicationSingleton.Instance;
            client.MessageArrived += ProcessMessage;
            client.SendCommandToServer(CommandEnum.GetConfigCommand, new string[] { });
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
                    NotifyPropertyChanged("Handlers");
                    break;
                case CommandEnum.RemoveHandler:
                    foreach (string handler in msg.Handlers)
                    {
                        Handlers.Remove(handler);
                    }
                    NotifyPropertyChanged("Handlers");
                    break;
                case CommandEnum.ConfigMessage:
                    OutputDir = msg.OutputDir;
                    SourceName = msg.LogSource;
                    LogName = msg.LogName;
                    ThumSize = msg.ThumbSize.ToString();
                    foreach (string handler in msg.Handlers)
                    {
                        Handlers.Add(handler);
                    }
                    NotifyPropertyChanged("Handlers");
                    break;
                default:
                    break;
            }
        }
        
        public void SendMessage(CommandEnum command, string[] args)
        {
            client.SendCommandToServer(command, args);
        }
    }
}
