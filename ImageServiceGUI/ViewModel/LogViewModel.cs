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
using ImageService.Infrastructure.Enums;
using ImageServiceGUI.Converters;

namespace ImageServiceGUI.ViewModel
{
    public class LogViewModel
    {
        private LogModel model;

        #region Properties
        public ObservableCollection<LogMessage> Logs { get; set; }
        public LogMessageTypeEnum Type { get; set; }
        public string Message { get; set; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public LogViewModel(LogModel model)
        {
            TypeToBrushConverter converter = new TypeToBrushConverter();
            Logs = new ObservableCollection<LogMessage>();
            this.model = model;
            this.model.Logs.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (LogMessage item in e.NewItems)
                        {
                            App.Current.Dispatcher.Invoke(delegate
                            {
                                Logs.Add(item);
                            });
                            //DataGridXAML.Items.Add(item);

                        }
                        NotifyPropertyChanged("Logs");
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (LogMessage item in e.OldItems)
                        {
                            App.Current.Dispatcher.Invoke(delegate
                            {
                                Logs.Remove(item);
                            });
                        }
                        NotifyPropertyChanged("Logs");
                        break;
                    default:
                        break;
                }
            };
        }

        public void NotifyPropertyChanged(string prop)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
