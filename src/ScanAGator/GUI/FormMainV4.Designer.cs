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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.imagesControl1 = new ScanAGator.GUI.ImagesControl();
            this.folderSelector1 = new ScanAGator.GUI.FolderSelectControl();
            this.analysisResultsControl = new ScanAGator.GUI.AnalysisResultsControl();
            this.analysisSettingsControl = new ScanAGator.GUI.AnalysisSettingsControl();
            this.notesControl1 = new ScanAGator.GUI.NotesControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 373F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.analysisResultsControl, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.analysisSettingsControl, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.notesControl1, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1219, 758);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.imagesControl1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.folderSelector1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 299F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(263, 754);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // imagesControl1
            // 
            this.imagesControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagesControl1.Location = new System.Drawing.Point(1, 456);
            this.imagesControl1.Margin = new System.Windows.Forms.Padding(1);
            this.imagesControl1.Name = "imagesControl1";
            this.imagesControl1.Size = new System.Drawing.Size(261, 297);
            this.imagesControl1.TabIndex = 3;
            // 
            // folderSelector1
            // 
            this.folderSelector1.AllowDrop = true;
            this.folderSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderSelector1.Location = new System.Drawing.Point(4, 5);
            this.folderSelector1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.folderSelector1.Name = "folderSelector1";
            this.folderSelector1.Size = new System.Drawing.Size(255, 445);
            this.folderSelector1.TabIndex = 1;
            // 
            // analysisResultsControl
            // 
            this.analysisResultsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analysisResultsControl.Location = new System.Drawing.Point(641, 1);
            this.analysisResultsControl.Margin = new System.Windows.Forms.Padding(1);
            this.analysisResultsControl.Name = "analysisResultsControl";
            this.analysisResultsControl.Size = new System.Drawing.Size(287, 756);
            this.analysisResultsControl.TabIndex = 2;
            // 
            // analysisSettingsControl
            // 
            this.analysisSettingsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analysisSettingsControl.Location = new System.Drawing.Point(271, 5);
            this.analysisSettingsControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.analysisSettingsControl.Name = "analysisSettingsControl";
            this.analysisSettingsControl.Size = new System.Drawing.Size(365, 748);
            this.analysisSettingsControl.TabIndex = 0;
            // 
            // notesControl1
            // 
            this.notesControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notesControl1.Location = new System.Drawing.Point(932, 3);
            this.notesControl1.Name = "notesControl1";
            this.notesControl1.Padding = new System.Windows.Forms.Padding(5);
            this.notesControl1.Size = new System.Drawing.Size(284, 752);
            this.notesControl1.TabIndex = 3;
            // 
            // FormMainV4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 758);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1241, 657);
            this.Name = "FormMainV4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan-A-Gator v4";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AnalysisSettingsControl analysisSettingsControl;
        private FolderSelectControl folderSelector1;
        private AnalysisResultsControl analysisResultsControl;
        private ImagesControl imagesControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private NotesControl notesControl1;
    }
}