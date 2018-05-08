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
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageServiceGUI.Communication;

namespace ImageServiceGUI.Model
{
    public class SettingsModel : IModel, INotifyPropertyChanged
    {
        #region Properties
        public ObservableCollection<string> Handlers { get; set; }
        #endregion

        #region Members
        private CommunicationSingleton client;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string handler)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(handler));
        }

        public SettingsModel()
        {
            Handlers = new ObservableCollection<string>();
            client = CommunicationSingleton.Instance;
            client.MessageArrived += ProcessMessage;
        }

        public void ProcessMessage(object sender, CommandMessageEventArgs e)
        {
            CommandMessage msg = e.Message;
            switch (msg.Type)
            {
                case CommandEnum.AddHandler:
                    Handlers.Add(msg.Handlers[0]);
                    OnPropertyChanged("Handlers");
                    break;
                case CommandEnum.ConfigMessage:
                    //fill
                    break;
                case CommandEnum.RemoveHandler:
                    Handlers.Remove(msg.Args[0]);
                    OnPropertyChanged("Handlers");
                    break;
                default:
                    break;
            }
        }
    }
}
