/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageServiceGUI.Model;
using Microsoft.Practices.Prism.Commands;
using System.Collections.Specialized;

namespace ImageServiceGUI.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private SettingsModel model;
        private DelegateCommand<object> removeHandler;
        public event PropertyChangedEventHandler PropertyChanged;
        public string outputDir { get; set; }
        public string sourceName { get; set; }
        public string logName { get; set; }
        public string thumSize { get; set; }
 
        public ObservableCollection<string> Handlers { get; set; }
        public string SelectedHandler { get; set; }

        public SettingsViewModel(SettingsModel model)
        {
            Handlers = new ObservableCollection<string>();
            this.model = model;
            outputDir = model.outputDir;
            sourceName = model.sourceName;
            logName = model.logName;
            thumSize = model.thumSize;
            this.model.Handlers.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (string item in e.NewItems)
                        {
                            Handlers.Add(item);
                        }
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Handlers"));
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (string item in e.OldItems)
                        {
                            Handlers.Remove(item);
                        }
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Handlers"));
                        break;
                    default:
                        break;
                }
            };
            Handlers = this.model.Handlers;
            removeHandler = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
        }

        public void NotifyPropertyChanged(string handler)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(handler));
        }

        private bool CanRemove(object obj)
        {
            if (Handlers.Count <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void OnRemove(object obj)
        {
            string[] args = new string[1];
            string handlerPath = SelectedHandler.ToString();
            args[0] = handlerPath;
            model.SendMessage("removeHandler", args);
        }
    }
}
