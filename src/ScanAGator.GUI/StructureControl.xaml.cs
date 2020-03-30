using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScanAGator.GUI
{
    public class Structures : INotifyPropertyChanged
    {
        int _b1 = 13;
        public int b1
        {
            get { return _b1; }
            set { _b1 = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }

    /// <summary>
    /// Interaction logic for StructureControl.xaml
    /// </summary>
    public partial class StructureControl : UserControl
    {
        Structures structures = new Structures();
        public StructureControl()
        {
            InitializeComponent();
            DataContext = structures;
            structures.b1 = 77;
        }

        private void StructureSlider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            structures.b1 = (int)e.NewValue;
        }
    }
}
