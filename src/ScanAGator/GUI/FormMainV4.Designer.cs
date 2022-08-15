namespace ScanAGator.GUI
{
    partial class FormMainV4
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
            this.imageRangeSelector1 = new ScanAGator.GUI.ScanSettings();
            this.folderSelector1 = new ScanAGator.GUI.FolderSelector();
            this.SuspendLayout();
            // 
            // imageRangeSelector1
            // 
            this.imageRangeSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.imageRangeSelector1.Location = new System.Drawing.Point(562, 20);
            this.imageRangeSelector1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.imageRangeSelector1.Name = "imageRangeSelector1";
            this.imageRangeSelector1.Size = new System.Drawing.Size(584, 891);
            this.imageRangeSelector1.TabIndex = 0;
            // 
            // folderSelector1
            // 
            this.folderSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.folderSelector1.Location = new System.Drawing.Point(13, 14);
            this.folderSelector1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.folderSelector1.Name = "folderSelector1";
            this.folderSelector1.Size = new System.Drawing.Size(539, 903);
            this.folderSelector1.TabIndex = 1;
            // 
            // FormMain2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1827, 931);
            this.Controls.Add(this.folderSelector1);
            this.Controls.Add(this.imageRangeSelector1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMain2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan-A-Gator v4";
            this.ResumeLayout(false);

        }

        #endregion

        private ScanSettings imageRangeSelector1;
        private FolderSelector folderSelector1;
    }
}