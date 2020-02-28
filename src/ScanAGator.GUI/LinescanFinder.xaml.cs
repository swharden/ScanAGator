using BitMiracle.LibTiff.Classic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ScanAGator.GUI
{
    /// <summary>
    /// Interaction logic for LinescanFinder.xaml
    /// </summary>
    public partial class LinescanFinder : Window
    {
        public LinescanFinder()
        {
            InitializeComponent();

            SetPath(@"X:\Data\OT-Cre\calcium-mannitol\2020-02-13 puff MT 2P");
            LoadLinescan(@"X:\Data\OT-Cre\calcium-mannitol\2020-02-13 puff MT 2P\20213000\mt-2");
        }

        private void SetPath(string path)
        {
            PathLabel.Content = path;
        }

        private void SetPath(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dialog.SelectedPath = PathLabel.Content.ToString();
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                PathLabel.Content = dialog.SelectedPath;
            }
        }

        private void ScanPath(object sender, RoutedEventArgs e)
        {
            string path = PathLabel.Content.ToString();
            if (!System.IO.Directory.Exists(path))
                throw new ArgumentException("path does not exist");

            SearchGroupbox.IsEnabled = false;
            SearchGroupbox.Header = "SCANNING...";
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ScanPathProcess(path)));

        }

        private void ScanPathProcess(string path)
        {
            string[] xmlFiles = System.IO.Directory.GetFiles(path, "LineScan-*.xml", System.IO.SearchOption.AllDirectories);

            Debug.WriteLine($"found {xmlFiles.Length} linescan XML files in: {path}");

            FolderListbox.Items.Clear();
            foreach (string xmlFilePath in xmlFiles)
            {
                Debug.WriteLine(xmlFilePath);
                FolderListbox.Items.Add(System.IO.Path.GetDirectoryName(xmlFilePath));
            }

            SearchGroupbox.IsEnabled = true;
            SearchGroupbox.Header = "Search Folder";
        }

        private void LinescanSelected(object sender, SelectionChangedEventArgs e)
        {
            if (FolderListbox.SelectedItem != null)
                LoadLinescan(FolderListbox.SelectedItem.ToString());
        }

        private static System.Drawing.Bitmap TiffToBitmap(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
                throw new ArgumentException("file does not exist: " + fileName);

            using Tiff tif = Tiff.Open(fileName, "r");

            int height = tif.GetField(TiffTag.IMAGELENGTH)[0].ToInt();
            int width = tif.GetField(TiffTag.IMAGEWIDTH)[0].ToInt();
            Debug.WriteLine($"image dimensions: {width} x {height}");

            FieldValue[] bitsPerSample = tif.GetField(TiffTag.BITSPERSAMPLE);
            short bpp = bitsPerSample[0].ToShort();
            Debug.WriteLine($"bits per pixel: {bpp}");

            FieldValue[] samplesPerPixel = tif.GetField(TiffTag.SAMPLESPERPIXEL);
            short spp = samplesPerPixel[0].ToShort();
            Debug.WriteLine($"samples per pixel: {spp}");

            int stride = tif.ScanlineSize();
            Debug.WriteLine($"scan line size: {stride}");

            System.Drawing.Bitmap result = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Debug.WriteLine($"result: {result} ({result.Width} x {result.Height})");

            for (int i = 0; i < height; i++)
            {
                var imgRect = new System.Drawing.Rectangle(0, i, width, 1);
                System.Drawing.Imaging.BitmapData imgData = result.LockBits(imgRect, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                byte[] buffer = new byte[stride];
                tif.ReadScanline(buffer, i);

                System.Runtime.InteropServices.Marshal.Copy(buffer, 0, imgData.Scan0, buffer.Length);
                result.UnlockBits(imgData);
            }

            return result;
        }

        private static BitmapImage BmpImageFromBmp(System.Drawing.Bitmap bmp)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage bmpImage = new BitmapImage();
            bmpImage.BeginInit();
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            bmpImage.StreamSource = stream;
            bmpImage.EndInit();
            return bmpImage;
        }

        private void LoadLinescan(string lineScanFolderPath)
        {
            var linescan = new ScanAGator.LineScanFolder(lineScanFolderPath);

            if (linescan.isValid == false)
            {
                wpfPlot1.Visibility = Visibility.Hidden;
                LinescanImage.Visibility = Visibility.Hidden;
                return;
            }
            else
            {
                wpfPlot1.Visibility = Visibility.Visible;
                LinescanImage.Visibility = Visibility.Visible;
            }

            LinescanImage.Source = BmpImageFromBmp(TiffToBitmap(linescan.pathsRef.Last()));

            (double[] redPositionIntensity, int px1, int px2, double noiseFloor) = linescan.AutoStructure();
            double widthMicrons = (px2 - px1) * linescan.micronsPerPx;

            wpfPlot1.plt.Clear();
            wpfPlot1.plt.Grid(enable: false);
            wpfPlot1.plt.Title($"Width Detection: {widthMicrons:0.00} µm");
            wpfPlot1.plt.XLabel("Position (µm)");
            wpfPlot1.plt.YLabel("Intensity (AFU)");
            wpfPlot1.plt.Ticks(fontSize: 8);

            wpfPlot1.plt.PlotSignal(redPositionIntensity, sampleRate: linescan.pixelsPerMicron, markerSize: 6, lineWidth: 3);
            wpfPlot1.plt.PlotVLine(px1 * linescan.micronsPerPx, color: System.Drawing.Color.Red, lineWidth: 2);
            wpfPlot1.plt.PlotVLine(px2 * linescan.micronsPerPx, color: System.Drawing.Color.Red, lineWidth: 2);
            wpfPlot1.plt.PlotHLine(0, color: System.Drawing.Color.Black, lineWidth: 2);
            wpfPlot1.plt.PlotHLine(noiseFloor, color: System.Drawing.Color.Black, lineWidth: 2, lineStyle: ScottPlot.LineStyle.Dot);

            wpfPlot1.plt.TightenLayout(20);
            wpfPlot1.plt.AxisAuto();
            wpfPlot1.Render();
        }
    }
}
