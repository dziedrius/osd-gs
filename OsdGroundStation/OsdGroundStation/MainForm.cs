using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using AForge.Video;
using AForge.Video.DirectShow;
using LayoutConfigurer.Model;
using tessnet2;

namespace OsdGroundStation
{
    public partial class MainForm : Form
    {
        private readonly Tesseract tesseract;
        private readonly TaskScheduler uiContext;

        private Stopwatch stopWatch;
        private int counter;

        public MainForm()
        {
            InitializeComponent();

            tesseract = new Tesseract();
            tesseract.SetVariable("tessedit_char_whitelist", "0123456789.");
            tesseract.Init("OcrData", "eng", false);

            uiContext = TaskScheduler.FromCurrentSynchronizationContext();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseCurrentVideoSource();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LocalVideoCaptureDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new VideoCaptureDeviceForm();

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                VideoCaptureDevice videoSource = form.VideoDevice;
                OpenVideoSource(videoSource);
            }
        }

        private void OpenVideofileusingDirectShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileSource = new FileVideoSource(OpenFileDialog.FileName);
                OpenVideoSource(fileSource);
            }
        }

        private void OpenJpegUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new UrlForm();

            form.Description = "Enter URL of an updating JPEG from a web camera:";
            form.Urls = new[]
                            {
                                "http://195.243.185.195/axis-cgi/jpg/image.cgi?camera=1" 
                            };

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                var jpegSource = new JPEGStream(form.Url);
                OpenVideoSource(jpegSource);
            }
        }

        private void OpenMJpegUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new UrlForm();

            form.Description = "Enter URL of an MJPEG video stream:";
            form.Urls = new[]
                            {
                                "http://195.243.185.195/axis-cgi/mjpg/video.cgi?camera=4", 
                                "http://195.243.185.195/axis-cgi/mjpg/video.cgi?camera=3" 
                            };

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                var mjpegSource = new MJPEGStream(form.Url);
                OpenVideoSource(mjpegSource);
            }
        }

        private void OpenVideoSource(IVideoSource source)
        {
            Cursor = Cursors.WaitCursor;

            CloseCurrentVideoSource();

            VideoSourcePlayer.VideoSource = source;
            VideoSourcePlayer.Start();

            stopWatch = null;

            Timer.Start();

            Cursor = Cursors.Default;
        }

        private void CloseCurrentVideoSource()
        {
            if (VideoSourcePlayer.VideoSource != null)
            {
                VideoSourcePlayer.SignalToStop();

                // wait ~ 3 seconds
                for (int i = 0; i < 30; i++)
                {
                    if (!VideoSourcePlayer.IsRunning)
                    {
                        break;
                    }

                    Thread.Sleep(100);
                }

                if (VideoSourcePlayer.IsRunning)
                {
                    VideoSourcePlayer.Stop();
                }

                VideoSourcePlayer.VideoSource = null;
            }
        }

        private void VideoSourcePlayer_NewFrame(object sender, ref Bitmap image)
        {

            var now = DateTime.Now;

            ReadData(image);

            var graphics = Graphics.FromImage(image);

            // paint current time
            using (var brush = new SolidBrush(Color.Red))
            {
                graphics.DrawString(now.ToString(), Font, brush, new PointF(5, 5));
            }


            if (CurrentLayout == null)
            {
                return;
            }

            using (var pen = new Pen(Color.Red))
            {
                foreach (var block in CurrentLayout.Blocks)
                {
                    graphics.DrawRectangle(pen, block.GetRect());
                }
            }

            graphics.Dispose();
        }

        private void ReadData(Bitmap image)
        {

            if (CurrentLayout == null)
            {
                return;
            }

            var lines = new List<string>(CurrentLayout.Blocks.Count);

            foreach (var layoutBlock in CurrentLayout.Blocks)
            {
                var firstWord = tesseract.DoOCR(MakeGrayscale(image), layoutBlock.GetRect()).First();
                lines.Add(string.Format("{0}: {1} ({2})", layoutBlock.Name, firstWord.Text,
                                        firstWord.Confidence));
            }

            Task.Factory.StartNew(() =>
                                      {
                                          DataTextBox.Lines = lines.ToArray();
                                      }, CancellationToken.None, TaskCreationOptions.None, uiContext);           
        }

        public static Bitmap MakeGrayscale(Bitmap original)
        {
            using (var gr = Graphics.FromImage(original))
            {
                //var grayMatrix = new[]
                //                      {
                //                          new float[] { 0.299f, 0.299f, 0.299f, 0, 0 },
                //                          new float[] { 0.587f, 0.587f, 0.587f, 0, 0 },
                //                          new float[] { 0.114f, 0.114f, 0.114f, 0, 0 },
                //                          new float[] { 0, 0, 0, 1, 0 },
                //                          new float[] { 0, 0, 0, 0, 1 }
                //                      };

                var grayMatrix = new[]
                                     {
                                         new float[] { 1.5f, 1.5f, 1.5f, 0, 0 },
                                         new float[] { 1.5f, 1.5f, 1.5f, 0, 0 },
                                         new float[] { 1.5f, 1.5f, 1.5f, 0, 0 },
                                         new float[] { 0, 0, 0, 1, 0 },
                                         new float[] { -1, -1, -1, 0, 1 }
                                     };

                var ia = new System.Drawing.Imaging.ImageAttributes();
                ia.SetColorMatrix(new System.Drawing.Imaging.ColorMatrix(grayMatrix));
                ia.SetThreshold(0.7f); // Change this threshold as needed
                var rc = new Rectangle(0, 0, original.Width, original.Height);
                gr.DrawImage(original, rc, 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, ia);
            }

            return original;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            IVideoSource videoSource = VideoSourcePlayer.VideoSource;

            if (videoSource != null)
            {
                // get number of frames since the last timer tick
                int framesReceived = videoSource.FramesReceived;

                if (stopWatch == null)
                {
                    stopWatch = new Stopwatch();
                    stopWatch.Start();
                }
                else
                {
                    stopWatch.Stop();

                    float fps = 1000.0f * framesReceived / stopWatch.ElapsedMilliseconds;
                    fpsLabel.Text = fps.ToString("F2") + " fps";

                    stopWatch.Reset();
                    stopWatch.Start();
                }
            }
        }

        private void OpenLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenLayoutFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var xmlSerializer = new XmlSerializer(typeof(Layout));

            using (var streamReader = new StreamReader(OpenLayoutFileDialog.FileName))
            {
                CurrentLayout = (Layout)xmlSerializer.Deserialize(streamReader);
            }
        }

        protected Layout CurrentLayout { get; set; }
    }
}