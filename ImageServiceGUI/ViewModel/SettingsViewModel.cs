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
        public DelegateCommand<object> RemoveHandler { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public string OutputDir
        {
            get { return model.OutputDir; }
        }
        public string SourceName
        {
            get { return model.SourceName; }
        }
        public string LogName
        {
            get { return model.LogName; }
        }
        public string ThumSize
        {
            get { return model.ThumSize; }
        }
 
        public ObservableCollection<string> Handlers { get; set; }
        public string SelectedHandler { get; set; }

        /// <summary>
        /// Constructor for SettingsViewModel class
        /// </summary>
        /// <param name="model">an instance of SettingsModel</param>
        public SettingsViewModel(SettingsModel model)
        {
            Handlers = new ObservableCollection<string>();
            this.model = model;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };

            this.model.Handlers.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (string item in e.NewItems)
                        {
                            if (string.IsNullOrWhiteSpace(item))
                            {
                                continue;
                            }
                            App.Current.Dispatcher.Invoke(delegate
                            {
                                Handlers.Add(item);
                            });
                        }
                        NotifyPropertyChanged("Handlers");
                        RemoveHandler.RaiseCanExecuteChanged();
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (string item in e.OldItems)
                        {
                            App.Current.Dispatcher.Invoke(delegate
                            {
                                Handlers.Remove(item);
                            });
                        }
                        NotifyPropertyChanged("Handlers");
                        RemoveHandler.RaiseCanExecuteChanged();
                        break;
                    default:
                        break;
                }
            };
            Handlers = new ObservableCollection<string>();
            RemoveHandler = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
        }

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
        /// The function is responsible of telling if a chosen handler
        /// can be removed.
        /// </summary>
        /// <param name="obj">unused</param>
        /// <returns>boolean value, true or false</returns>
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

        /// <summary>
        /// The function is responsible of telling what is done
        /// when removing a handler.
        /// </summary>
        /// <param name="obj">unused</param>
        private void OnRemove(object obj)
        {
            string[] args = new string[1];
            string handlerPath = SelectedHandler.ToString();
            args[0] = handlerPath;
            model.SendMessage("removeHandler", args);
        }
    }
}
