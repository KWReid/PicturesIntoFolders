namespace PicturesIntoFolders
{
    partial class PicturesIntoFoldersForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PicturesIntoFoldersForm));
      this.m_SelectFilesButton = new System.Windows.Forms.Button();
      this.m_DropGroupBox = new System.Windows.Forms.GroupBox();
      this.m_DropTargetPanel = new System.Windows.Forms.Panel();
      this.FileBrowserDialog = new System.Windows.Forms.OpenFileDialog();
      this.m_LogTextbox = new System.Windows.Forms.TextBox();
      this.m_DropGroupBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_SelectFilesButton
      // 
      this.m_SelectFilesButton.Location = new System.Drawing.Point(12, 12);
      this.m_SelectFilesButton.Name = "m_SelectFilesButton";
      this.m_SelectFilesButton.Size = new System.Drawing.Size(75, 69);
      this.m_SelectFilesButton.TabIndex = 0;
      this.m_SelectFilesButton.Text = "Select Files";
      this.m_SelectFilesButton.UseVisualStyleBackColor = true;
      this.m_SelectFilesButton.Click += new System.EventHandler(this.SelectFilesButton_Click);
      // 
      // m_DropGroupBox
      // 
      this.m_DropGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_DropGroupBox.Controls.Add(this.m_DropTargetPanel);
      this.m_DropGroupBox.Location = new System.Drawing.Point(94, 7);
      this.m_DropGroupBox.Name = "m_DropGroupBox";
      this.m_DropGroupBox.Size = new System.Drawing.Size(236, 74);
      this.m_DropGroupBox.TabIndex = 1;
      this.m_DropGroupBox.TabStop = false;
      this.m_DropGroupBox.Text = "Drop Files Here";
      // 
      // m_DropTargetPanel
      // 
      this.m_DropTargetPanel.AllowDrop = true;
      this.m_DropTargetPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_DropTargetPanel.Location = new System.Drawing.Point(3, 16);
      this.m_DropTargetPanel.Name = "m_DropTargetPanel";
      this.m_DropTargetPanel.Size = new System.Drawing.Size(230, 55);
      this.m_DropTargetPanel.TabIndex = 0;
      this.m_DropTargetPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_DropTargetPanel_DragDrop);
      this.m_DropTargetPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_DropTargetPanel_DragEnter);
      // 
      // FileBrowserDialog
      // 
      this.FileBrowserDialog.Filter = "Jpeg files|*.jpg|All files|*.*";
      this.FileBrowserDialog.Multiselect = true;
      this.FileBrowserDialog.Title = "Select the files to be moved...";
      // 
      // m_LogTextbox
      // 
      this.m_LogTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_LogTextbox.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.m_LogTextbox.Location = new System.Drawing.Point(12, 88);
      this.m_LogTextbox.Multiline = true;
      this.m_LogTextbox.Name = "m_LogTextbox";
      this.m_LogTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.m_LogTextbox.Size = new System.Drawing.Size(318, 134);
      this.m_LogTextbox.TabIndex = 2;
      // 
      // PicturesIntoFoldersForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(342, 234);
      this.Controls.Add(this.m_LogTextbox);
      this.Controls.Add(this.m_DropGroupBox);
      this.Controls.Add(this.m_SelectFilesButton);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "PicturesIntoFoldersForm";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Picture Mover";
      this.m_DropGroupBox.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_SelectFilesButton;
        private System.Windows.Forms.GroupBox m_DropGroupBox;
        private System.Windows.Forms.OpenFileDialog FileBrowserDialog;
        private System.Windows.Forms.TextBox m_LogTextbox;
        private System.Windows.Forms.Panel m_DropTargetPanel;
    }
}

