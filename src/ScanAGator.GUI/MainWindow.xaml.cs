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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScanAGator.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Version ver = typeof(ScanAGator.LineScanFolder).Assembly.GetName().Version;
            VersionLabel.Content = $"version {ver.Major}.{ver.Minor}";

            OriginalViewerButton.IsEnabled = System.IO.File.Exists("oldViewer/ScanAGator.exe");

            // open new app on launch
            LaunchNewViewer(null, null);
        }

        private void LaunchLinescanFinder(object sender, RoutedEventArgs e)
        {
            new LinescanFinder().ShowDialog();
        }

        private void LaunchOriginalViewer(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("oldViewer/ScanAGator.exe");
        }

        private void LaunchNewViewer(object sender, RoutedEventArgs e)
        {
            new LinescanEditor().ShowDialog();
        }
    }
}
