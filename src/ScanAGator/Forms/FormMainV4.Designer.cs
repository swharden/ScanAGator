using ScanAGator.Controls;

namespace ScanAGator.Forms
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.imagesControl1 = new ScanAGator.Controls.ImagesControl();
            this.folderSelector1 = new ScanAGator.Controls.FolderSelectControl();
            this.analysisResultsControl = new ScanAGator.Controls.AnalysisResultsControl();
            this.analysisSettingsControl = new ScanAGator.Controls.AnalysisSettingsControl();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.imagesControl1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.folderSelector1, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 460F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(394, 1204);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // imagesControl1
            // 
            this.imagesControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagesControl1.Location = new System.Drawing.Point(2, 746);
            this.imagesControl1.Margin = new System.Windows.Forms.Padding(2);
            this.imagesControl1.Name = "imagesControl1";
            this.imagesControl1.Size = new System.Drawing.Size(390, 456);
            this.imagesControl1.TabIndex = 3;
            // 
            // folderSelector1
            // 
            this.folderSelector1.AllowDrop = true;
            this.folderSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderSelector1.Location = new System.Drawing.Point(6, 8);
            this.folderSelector1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.folderSelector1.Name = "folderSelector1";
            this.folderSelector1.Size = new System.Drawing.Size(382, 728);
            this.folderSelector1.TabIndex = 1;
            // 
            // analysisResultsControl
            // 
            this.analysisResultsControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analysisResultsControl.Location = new System.Drawing.Point(1594, 11);
            this.analysisResultsControl.Margin = new System.Windows.Forms.Padding(2);
            this.analysisResultsControl.Name = "analysisResultsControl";
            this.analysisResultsControl.Size = new System.Drawing.Size(692, 1203);
            this.analysisResultsControl.TabIndex = 2;
            // 
            // analysisSettingsControl
            // 
            this.analysisSettingsControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analysisSettingsControl.Location = new System.Drawing.Point(415, 12);
            this.analysisSettingsControl.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.analysisSettingsControl.Name = "analysisSettingsControl";
            this.analysisSettingsControl.Size = new System.Drawing.Size(1171, 1204);
            this.analysisSettingsControl.TabIndex = 0;
            // 
            // FormMainV4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2297, 1228);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.analysisSettingsControl);
            this.Controls.Add(this.analysisResultsControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1850, 981);
            this.Name = "FormMainV4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan-A-Gator v4";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AnalysisSettingsControl analysisSettingsControl;
        private FolderSelectControl folderSelector1;
        private AnalysisResultsControl analysisResultsControl;
        private ImagesControl imagesControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}