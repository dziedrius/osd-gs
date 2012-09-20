using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using LayoutConfigurer.Model;
using tessnet2;

namespace LayoutConfigurer
{
    public partial class MainForm : Form
    {
        private readonly Tesseract tesseract;

        public MainForm()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                var block = (LayoutBlock)LayoutBlockBindingSource.AddNew();
                SetDefaultBlockValues(block);
                LayoutBlockBindingSource.EndEdit();

                Debug.Assert(block != null, "block != null");
                CurrentDisplayBlock = new DisplayBlock(Rectangle.Empty);
                CurrentDisplayBlock.SetPictureBox(PictureBox);
                CurrentDisplayBlock.SizeChanged += CurrentDisplayBlock_SizeChanged;

                tesseract = new Tesseract();
                tesseract.SetVariable("tessedit_char_whitelist", "0123456789.");
                tesseract.Init("OcrData", "eng", false);
            }
        }

        void CurrentDisplayBlock_SizeChanged(object sender, DisplayBlockSizeChangedHandlerArgs args)
        {
            CurrentLayoutBlock.SetRect(CurrentDisplayBlock.GetRect());
            LayoutBlockBindingSource.EndEdit();
            LayoutBlocksDataGridView.EndEdit();
            LayoutBlocksDataGridView.Refresh();
        }

        public DisplayBlock CurrentDisplayBlock { get; set; }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ImageOpenFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            
            var image = Image.FromFile(ImageOpenFileDialog.FileName);

            PictureBox.Image = image;
            PictureBox.Width = image.Width;
            PictureBox.Height = image.Height;

            CurrentDisplayBlock.SetRect(CurrentLayoutBlock.GetRect());            
        }

        private void LoadLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LayoutOpenFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var xmlSerializer = new XmlSerializer(typeof(Layout));
            
            using (var streamReader = new StreamReader(LayoutOpenFileDialog.FileName))
            {
                var layout = (Layout)xmlSerializer.Deserialize(streamReader);
                LayoutBlockBindingSource.DataSource = layout.Blocks;
            }
        }      

        private void SetDefaultBlockValues(LayoutBlock block)
        {
            block.Left = (PictureBox.Width / 2) - 50;
            block.Top = (PictureBox.Height / 2) - 50;
            block.Width = 100;
            block.Height = 100;
            block.Enabled = true;
        }

        private void LayoutBlockBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (PictureBox.Image == null)
            {
                return;
            }

            if (CurrentLayoutBlock != null)
            {
                CurrentDisplayBlock.SetRect(CurrentLayoutBlock.GetRect());
            }
        }

        private LayoutBlock CurrentLayoutBlock
        {
            get { return (LayoutBlock)LayoutBlockBindingSource.Current; }
        }       

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (PictureBox.Image == null)
            {
                return;
            }

            var graphics = e.Graphics;
            foreach (var block in LayoutBlockBindingSource.OfType<LayoutBlock>())
            {
                graphics.DrawRectangle(new Pen(Color.Green), block.GetRect());
            }

            if (CurrentDisplayBlock != null)
            {
                CurrentDisplayBlock.Draw(graphics);
            }
        }

        private void AddNewBlockButton_Click(object sender, EventArgs e)
        {
            var block = (LayoutBlock)LayoutBlockBindingSource.AddNew();
            SetDefaultBlockValues(block);
            LayoutBlockBindingSource.EndEdit();
            
            Debug.Assert(block != null, "block != null");
            CurrentDisplayBlock.SetRect(block.GetRect());            
        }

        private void SaveLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var xmlSerializer = new XmlSerializer(typeof(Layout));
            using (var streamWriter = new StreamWriter(SaveFileDialog.FileName))
            {
                xmlSerializer.Serialize(streamWriter, new Layout
                                            {
                                                Blocks = LayoutBlockBindingSource.OfType<LayoutBlock>().ToList()
                                            });
            }
        }

        private void TestOcrButton_Click(object sender, EventArgs e)
        {
            var bitmap = (Bitmap)PictureBox.Image;
            var word = tesseract.DoOCR(bitmap, CurrentLayoutBlock.GetRect()).First();
            TestOcrLabel.Text = string.Format("Test Ocr: word - {0}, confidence - {1}", word.Text, word.Confidence);
        }

        public static Bitmap MakeGrayscale(Bitmap original)
        {
            using (var gr = Graphics.FromImage(original)) 
            {
                var grayMatrix = new[]
                                      {
                                          new float[] { 0.299f, 0.299f, 0.299f, 0, 0 },
                                          new float[] { 0.587f, 0.587f, 0.587f, 0, 0 },
                                          new float[] { 0.114f, 0.114f, 0.114f, 0, 0 },
                                          new float[] { 0, 0, 0, 1, 0 },
                                          new float[] { 0, 0, 0, 0, 1 }
                                      };

                var ia = new ImageAttributes();
                ia.SetColorMatrix(new ColorMatrix(grayMatrix));
                ia.SetThreshold(0.8f); // Change this threshold as needed
                var rc = new Rectangle(0, 0, original.Width, original.Height);
                gr.DrawImage(original, rc, 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, ia);
            }

            return original;
        }
    }
}
