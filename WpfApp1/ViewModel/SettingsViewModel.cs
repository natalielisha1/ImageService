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
using ImageServiceGUI.Model;

namespace ImageServiceGUI.ViewModel
{
    public class SettingsViewModel
    {
        private ISettingsModel model;
        public SettingsViewModel(ISettingsModel model)
        {
            this.model = model;
        }

        /*public string ServerIP
        {
            get { return model.ServerIP; }
            set
            {
                model.ServerIP = value;
            }
        }
        public int ServerPort
        {
            get { return model.ServerPort; }
            set
            {
                model.ServerPort = value;
            }
        }*/

        public void SaveSettings()
        {
            model.SaveSettings();
        }
    }
}
