/**
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
    /// Interaction logic for LogView.xaml
    /// </summary>
    public partial class LogView : UserControl
    {
        private LogViewModel vm;

        /// <summary>
        /// Constructor for LogView class
        /// </summary>
        public LogView()
        {
            InitializeComponent();
            LogModel m = new LogModel();
            vm = new LogViewModel(m);
            this.DataContext = vm;
        }
    }
}
