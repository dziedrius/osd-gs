using System;
using System.Windows.Forms;

namespace OsdGroundStation
{
    public partial class UrlForm : Form
    {
        private string url;
       
        public string Url
        {
            get { return url; }
        }
        
        public string[] Urls
        {
            set
            {                
                UrlComboBox.Items.AddRange(value);
            }
        }
        
        public string Description
        {
            get { return DescriptionLabel.Text; }
            set { DescriptionLabel.Text = value; }
        }
        
        public UrlForm()
        {
            InitializeComponent();
        }
        
        private void OkButton_Click(object sender, EventArgs e)
        {
            url = UrlComboBox.Text;
        }
    }
}
