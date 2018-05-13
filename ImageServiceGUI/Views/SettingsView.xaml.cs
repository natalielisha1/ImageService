/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */

using ImageServiceGUI.Model;
using ImageServiceGUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageServiceGUI.Views
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SettingsView
    {
        private SettingsViewModel vm;
        private string outputDir;
        private string sourceName;
        private string logName;
        private string thumSize;
        public ObservableCollection<string> Handlers { get; set; }

        public SettingsView()
        {
            InitializeComponent();
            SettingsModel m = new SettingsModel();
            vm = new SettingsViewModel(m);
            Handlers = vm.Handlers;
            this.DataContext = this;
            this.outputDir = vm.outputDir;
            this.sourceName = vm.sourceName;
            this.logName = vm.logName;
            this.thumSize = vm.thumSize;
            handlersList.ItemsSource = this.Handlers;
            //newHandlers = new ObservableCollection<string>();
            //newHandlers.Add("first handler");
            //newHandlers.Add("second handler");
            //newHandlers.Add("third handler");
        }
        private void btnRemoveHandler_Click(object sender, RoutedEventArgs e)
        {
            //vm.RemoveHandler(...);
            if (handlersList.SelectedItem != null)
            {
                Handlers.Remove(handlersList.SelectedItem as string);
                //here I want to send removeHandler command to effect the model and vm handlers as well
            }        }
    }
}
