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
using Newtonsoft.Json.Linq;
using ImageService.Commands;
using ImageService.Modal;
using Microsoft.Practices.Prism.Commands;
using ImageService.Controller.Handlers;
using ImageService.Controller;
using ImageService.Logging;
using System.Collections.Specialized;

namespace ImageServiceGUI.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private SettingsModel model;
        private DelegateCommand<object> removeHandler;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> Handlers { get; set; }

        public SettingsViewModel(SettingsModel model)
        {
            Handlers = new ObservableCollection<string>();
            this.model = model;
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
            this.Handlers = this.model.Handlers;
            this.removeHandler = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
        }

        public void NotifyPropertyChanged(string handler)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(handler));
        }

        private bool CanRemove(object obj)
        {
            if (this.Handlers.Count <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void OnRemove(object handlerPath)
        {
            this.Handlers = this.model.Handlers;
        }
    }
}
