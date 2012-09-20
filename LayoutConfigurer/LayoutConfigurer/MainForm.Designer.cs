namespace LayoutConfigurer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.LayoutBlocksDataGridView = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.leftDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.topDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.widthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heightDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LayoutBlockBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AddNewBlockButton = new System.Windows.Forms.Button();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.LayoutOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.TestOcrButton = new System.Windows.Forms.Button();
            this.TestOcrLabel = new System.Windows.Forms.Label();
            this.MainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutBlocksDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutBlockBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(729, 24);
            this.MainMenuStrip.TabIndex = 0;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveLayoutToolStripMenuItem,
            this.LoadLayoutToolStripMenuItem,
            this.LoadImageToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // SaveLayoutToolStripMenuItem
            // 
            this.SaveLayoutToolStripMenuItem.Name = "SaveLayoutToolStripMenuItem";
            this.SaveLayoutToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.SaveLayoutToolStripMenuItem.Text = "Save Layout...";
            this.SaveLayoutToolStripMenuItem.Click += new System.EventHandler(this.SaveLayoutToolStripMenuItem_Click);
            // 
            // LoadLayoutToolStripMenuItem
            // 
            this.LoadLayoutToolStripMenuItem.Name = "LoadLayoutToolStripMenuItem";
            this.LoadLayoutToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.LoadLayoutToolStripMenuItem.Text = "Load Layout...";
            this.LoadLayoutToolStripMenuItem.Click += new System.EventHandler(this.LoadLayoutToolStripMenuItem_Click);
            // 
            // LoadImageToolStripMenuItem
            // 
            this.LoadImageToolStripMenuItem.Name = "LoadImageToolStripMenuItem";
            this.LoadImageToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.LoadImageToolStripMenuItem.Text = "Load Image...";
            this.LoadImageToolStripMenuItem.Click += new System.EventHandler(this.LoadImageToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // ImageOpenFileDialog
            // 
            this.ImageOpenFileDialog.Filter = "Image files|*.png;*.jpg;*.bmp;*.gif|All files|*.*";
            // 
            // PictureBox
            // 
            this.PictureBox.Location = new System.Drawing.Point(0, 27);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(720, 576);
            this.PictureBox.TabIndex = 1;
            this.PictureBox.TabStop = false;
            this.PictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox_Paint);
            // 
            // LayoutBlocksDataGridView
            // 
            this.LayoutBlocksDataGridView.AllowUserToAddRows = false;
            this.LayoutBlocksDataGridView.AutoGenerateColumns = false;
            this.LayoutBlocksDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LayoutBlocksDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.enabledDataGridViewCheckBoxColumn,
            this.leftDataGridViewTextBoxColumn,
            this.topDataGridViewTextBoxColumn,
            this.widthDataGridViewTextBoxColumn,
            this.heightDataGridViewTextBoxColumn});
            this.LayoutBlocksDataGridView.DataSource = this.LayoutBlockBindingSource;
            this.LayoutBlocksDataGridView.Location = new System.Drawing.Point(0, 609);
            this.LayoutBlocksDataGridView.Name = "LayoutBlocksDataGridView";
            this.LayoutBlocksDataGridView.Size = new System.Drawing.Size(720, 113);
            this.LayoutBlocksDataGridView.TabIndex = 2;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // enabledDataGridViewCheckBoxColumn
            // 
            this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
            this.enabledDataGridViewCheckBoxColumn.HeaderText = "Enabled";
            this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
            // 
            // leftDataGridViewTextBoxColumn
            // 
            this.leftDataGridViewTextBoxColumn.DataPropertyName = "Left";
            this.leftDataGridViewTextBoxColumn.HeaderText = "Left";
            this.leftDataGridViewTextBoxColumn.Name = "leftDataGridViewTextBoxColumn";
            // 
            // topDataGridViewTextBoxColumn
            // 
            this.topDataGridViewTextBoxColumn.DataPropertyName = "Top";
            this.topDataGridViewTextBoxColumn.HeaderText = "Top";
            this.topDataGridViewTextBoxColumn.Name = "topDataGridViewTextBoxColumn";
            // 
            // widthDataGridViewTextBoxColumn
            // 
            this.widthDataGridViewTextBoxColumn.DataPropertyName = "Width";
            this.widthDataGridViewTextBoxColumn.HeaderText = "Width";
            this.widthDataGridViewTextBoxColumn.Name = "widthDataGridViewTextBoxColumn";
            // 
            // heightDataGridViewTextBoxColumn
            // 
            this.heightDataGridViewTextBoxColumn.DataPropertyName = "Height";
            this.heightDataGridViewTextBoxColumn.HeaderText = "Height";
            this.heightDataGridViewTextBoxColumn.Name = "heightDataGridViewTextBoxColumn";
            // 
            // LayoutBlockBindingSource
            // 
            this.LayoutBlockBindingSource.DataSource = typeof(LayoutConfigurer.Model.LayoutBlock);
            this.LayoutBlockBindingSource.CurrentChanged += new System.EventHandler(this.LayoutBlockBindingSource_CurrentChanged);
            // 
            // AddNewBlockButton
            // 
            this.AddNewBlockButton.Location = new System.Drawing.Point(0, 723);
            this.AddNewBlockButton.Name = "AddNewBlockButton";
            this.AddNewBlockButton.Size = new System.Drawing.Size(75, 23);
            this.AddNewBlockButton.TabIndex = 3;
            this.AddNewBlockButton.Text = "Add block";
            this.AddNewBlockButton.UseVisualStyleBackColor = true;
            this.AddNewBlockButton.Click += new System.EventHandler(this.AddNewBlockButton_Click);
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.DefaultExt = "xml";
            this.SaveFileDialog.Filter = "Xml files|*.xml|All files|*.*";
            // 
            // LayoutOpenFileDialog
            // 
            this.LayoutOpenFileDialog.DefaultExt = "xml";
            this.LayoutOpenFileDialog.Filter = "Xml files|*.xml|All files|*.*";
            // 
            // TestOcrButton
            // 
            this.TestOcrButton.Location = new System.Drawing.Point(81, 723);
            this.TestOcrButton.Name = "TestOcrButton";
            this.TestOcrButton.Size = new System.Drawing.Size(75, 23);
            this.TestOcrButton.TabIndex = 4;
            this.TestOcrButton.Text = "Test Ocr";
            this.TestOcrButton.UseVisualStyleBackColor = true;
            this.TestOcrButton.Click += new System.EventHandler(this.TestOcrButton_Click);
            // 
            // label1
            // 
            this.TestOcrLabel.AutoSize = true;
            this.TestOcrLabel.Location = new System.Drawing.Point(162, 728);
            this.TestOcrLabel.Name = "TestOcrLabel";
            this.TestOcrLabel.Size = new System.Drawing.Size(116, 13);
            this.TestOcrLabel.TabIndex = 5;
            this.TestOcrLabel.Text = "Test Ocr Value: (None)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 750);
            this.Controls.Add(this.TestOcrLabel);
            this.Controls.Add(this.TestOcrButton);
            this.Controls.Add(this.AddNewBlockButton);
            this.Controls.Add(this.LayoutBlocksDataGridView);
            this.Controls.Add(this.PictureBox);
            this.Controls.Add(this.MainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Layout Configurer";
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutBlocksDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutBlockBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ImageOpenFileDialog;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.BindingSource LayoutBlockBindingSource;
        private System.Windows.Forms.ToolStripMenuItem LoadLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveLayoutToolStripMenuItem;
        private System.Windows.Forms.DataGridView LayoutBlocksDataGridView;        
        private System.Windows.Forms.Button AddNewBlockButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn leftDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn topDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn widthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn heightDataGridViewTextBoxColumn;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.OpenFileDialog LayoutOpenFileDialog;
        private System.Windows.Forms.Button TestOcrButton;
        private System.Windows.Forms.Label TestOcrLabel;    
    }
}

