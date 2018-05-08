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

namespace ImageServiceGUI.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private ISettingsModel model;
        private DelegateCommand<object> removeHandler;
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<DirectoryHandler> handlers;

        public SettingsViewModel(ISettingsModel model)
        {
            this.model = model;
            this.handlers = new ObservableCollection<DirectoryHandler>();
            this.removeHandler = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
            this.AddHandlers();
        }

        private void AddHandlers()
        {
            //getting the configs and creating controller and model
            bool result = true;
            IImageServiceModal temp_model = new ImageServiceModal();
            ILoggingService logging = new LoggingService();
            GetConfigCommand configCommand = new GetConfigCommand(temp_model);
            string[] args = null;
            string configs = configCommand.Execute(args, out result);
            IImageController controller = new ImageController(temp_model);

            //splitting the paths of the handlers
            string allHandlers = configs; //TODO: fix later, allHandlers sould have the value of the key "Handler"
            string[] paths = allHandlers.Split(';');

            //adding the directories to the observable collection
            for (int i = 0; i < paths.Length; i++)
            {
                this.handlers.Add(new DirectoryHandler(paths[i], controller, logging));
            }
        }

        public void NotifyPropertyChanged(string handler)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(handler));
        }

        private bool CanRemove(object obj)
        {
            if (this.handlers.Count <= 0)
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
            model.RemoveHandler((string)handlerPath); //dangerous?
            //in remove handler, remove the handler from the app config
            this.NotifyPropertyChanged((string)handlerPath);
        }

        public string ToJSON()
        {
            JObject settingsObj = new JObject();
            bool result = true;
            IImageServiceModal temp_model = new ImageServiceModal();
            GetConfigCommand configCommand = new GetConfigCommand(temp_model);
            string[] args = null;
            string configs = configCommand.Execute(args, out result);
            settingsObj["OutputDir"] = configs[1];
            settingsObj["SourceName"] = configs[2];
            settingsObj["LogName"] = configs[3];
            settingsObj["ThumbnailSize"] = configs[4];
            //TODO: fix later
            return settingsObj.ToString();
        }
    }
}
