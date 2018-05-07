/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageServiceGUI.Model;

namespace ImageServiceGUI.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private ISettingsModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel(ISettingsModel model)
        {
            this.model = model;
        }

        public void NotifyPropertyChanged(string handler)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(handler));
        }
        public void RemoveHandler(string handlerPath)
        {
            model.RemoveHandler(handlerPath);
            this.NotifyPropertyChanged(handlerPath);
        }
    }
}
