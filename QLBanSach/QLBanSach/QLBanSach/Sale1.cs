using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace QLBanSach
{
    public partial class Sale1 : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        public Sale1()
        {
            InitializeComponent();
            this.button3.Click += Button3_Click;
            this.button4.Click += Button4_Click;
            //khoi tao thiet bi
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo info in filterInfoCollection)
            {
                comboBox1.Items.Add(info.Name);
            }
            comboBox1.SelectedIndex = 0;
            this.label1.Text = DateTime.Now.ToString();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.Stop();
                videoCaptureDevice = null;
                pictureBox1.Image = null;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if(videoCaptureDevice!=null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.Stop();
            }
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboBox1.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }

        

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);
            if(result != null)
            {
                textBox2.Invoke(new MethodInvoker(delegate ()
                {
                    textBox2.Text = result.ToString();
                }));
            }
            pictureBox1.Image = bitmap;
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
