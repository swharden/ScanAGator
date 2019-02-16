using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageModuleTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var img1 = new ImageData(@"C:\Users\scott\Documents\GitHub\Scan-A-Gator\data\linescans\LineScan-02052019-1234-2683\LineScan-02052019-1234-2683-Cycle00001_Ch2Source.tif");
            var img2 = new ImageData(@"C:\Users\scott\Documents\GitHub\Scan-A-Gator\data\linescans\LineScan-02052019-1234-2683\References\LineScan-02052019-1234-2683-Cycle00002-Ch1-16bit-Reference.tif");
            var img3 = new ImageData(@"C:\Users\scott\Documents\GitHub\Scan-A-Gator\data\linescans\LineScan-02052019-1234-2683\References\LineScan-02052019-1234-2683-Cycle00002-Window1-Ch1-8bit-Reference.tif");
            pictureBox1.BackgroundImage = img1.GetBmpDisplay();
            pictureBox2.BackgroundImage = img2.GetBmpDisplay();
            pictureBox3.BackgroundImage = img3.GetBmpDisplay();
        }
    }
}
