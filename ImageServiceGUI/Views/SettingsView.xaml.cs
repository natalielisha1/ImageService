﻿/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */

using ImageServiceGUI.Model;
using ImageServiceGUI.ViewModel;
using System;
using System.Collections.Generic;
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
        public SettingsView()
        {
            InitializeComponent();
            SettingsModel m = new SettingsModel();
            vm = new SettingsViewModel(m);
            this.DataContext = vm;
        }
        private void btnRemoveHandler_Click(object sender, RoutedEventArgs e)
        {
            //vm.RemoveHandler(...);
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
        }
    }
}
