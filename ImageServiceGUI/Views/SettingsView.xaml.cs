/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */

using ImageServiceGUI.Model;
using ImageServiceGUI.ViewModel;

namespace ImageServiceGUI.Views
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SettingsView
    {
        private SettingsViewModel vm;
        public SettingsView()
        {
            InitializeComponent();
            SettingsModel m = new SettingsModel();
            vm = new SettingsViewModel(m);
            this.DataContext = this.vm;
            //handlersList.ItemsSource = vm.Handlers;
        }
    }
}
