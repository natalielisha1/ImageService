/**
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
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string log)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(log));
        }

        public LogModel()
        {
            Logs = new ObservableCollection<LogMessage>();
            client = CommunicationSingleton.Instance;
            client.MessageArrived += ProcessMessage;
        }

        public void ProcessMessage(object sender, CommandMessageEventArgs e)
        {
            CommandMessage msg = e.Message;
            switch (msg.Type)
            {
                case CommandEnum.LogAdded:
                    foreach (LogMessage log in msg.LogMessages)
                    {
                        Logs.Add(log);
                        OnPropertyChanged("Logs");
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
