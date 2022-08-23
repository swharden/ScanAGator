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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainV4));
            this.analysisSettingsControl = new ScanAGator.GUI.AnalysisSettingsControl();
            this.folderSelector1 = new ScanAGator.GUI.FolderSelectControl();
            this.analysisResultsControl = new ScanAGator.GUI.AnalysisResultsControl();
            this.SuspendLayout();
            // 
            // analysisSettingsControl
            // 
            this.analysisSettingsControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.analysisSettingsControl.Location = new System.Drawing.Point(375, 13);
            this.analysisSettingsControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.analysisSettingsControl.Name = "analysisSettingsControl";
            this.analysisSettingsControl.Size = new System.Drawing.Size(389, 579);
            this.analysisSettingsControl.TabIndex = 0;
            // 
            // folderSelector1
            // 
            this.folderSelector1.AllowDrop = true;
            this.folderSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.folderSelector1.Location = new System.Drawing.Point(9, 9);
            this.folderSelector1.Name = "folderSelector1";
            this.folderSelector1.Size = new System.Drawing.Size(359, 587);
            this.folderSelector1.TabIndex = 1;
            // 
            // analysisResultsControl
            // 
            this.analysisResultsControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analysisResultsControl.Location = new System.Drawing.Point(770, 8);
            this.analysisResultsControl.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.analysisResultsControl.Name = "analysisResultsControl";
            this.analysisResultsControl.Size = new System.Drawing.Size(440, 590);
            this.analysisResultsControl.TabIndex = 2;
            // 
            // FormMainV4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 605);
            this.Controls.Add(this.analysisResultsControl);
            this.Controls.Add(this.folderSelector1);
            this.Controls.Add(this.analysisSettingsControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMainV4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan-A-Gator v4";
            this.ResumeLayout(false);

        }

        #endregion

        private AnalysisSettingsControl analysisSettingsControl;
        private FolderSelectControl folderSelector1;
        private AnalysisResultsControl analysisResultsControl;
    }
}