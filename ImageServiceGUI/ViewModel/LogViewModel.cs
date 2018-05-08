/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using System.Collections.Specialized;
using ImageServiceGUI.Model;
using ImageService.Communication.Model;

namespace ImageServiceGUI.ViewModel
{
    public class LogViewModel
    {
        private LogModel model;
        //private DelegateCommand<object> removeHandler;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<LogMessage> Logs { get; set; }

        public LogViewModel(LogModel model)
        {
            Logs = new ObservableCollection<LogMessage>();
            this.model = model;
            this.model.Logs.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (LogMessage item in e.NewItems)
                        {
                            Logs.Add(item);
                        }
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Logs"));
                        break;
                    //case NotifyCollectionChangedAction.Remove:
                      //  foreach (string item in e.OldItems)
                       // {
                        //    Logs.Remove(item);
                        //}
                        //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Logs"));
                        //break;
                    default:
                        break;
                }
            };
            this.Logs = this.model.Logs;
            //this.removeHandler = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
        }

        public void NotifyPropertyChanged(string handler)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(handler));
        }
    }
}
