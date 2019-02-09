namespace ScanAGator
{
    partial class FormMain
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.pbRef = new System.Windows.Forms.PictureBox();
            this.pbData = new System.Windows.Forms.PictureBox();
            this.tbBaseline1 = new System.Windows.Forms.TrackBar();
            this.gbBaseline = new System.Windows.Forms.GroupBox();
            this.tbBaseline2 = new System.Windows.Forms.TrackBar();
            this.gbStructure = new System.Windows.Forms.GroupBox();
            this.tbStructure2 = new System.Windows.Forms.TrackBar();
            this.tbStructure1 = new System.Windows.Forms.TrackBar();
            this.scottPlotUC1 = new ScottPlotDev2.ScottPlotUC();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioPMT = new System.Windows.Forms.RadioButton();
            this.radioGoR = new System.Windows.Forms.RadioButton();
            this.radioDeltaGoR = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblFilterMs = new System.Windows.Forms.Label();
            this.nudFilter = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblPeak = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioImageG = new System.Windows.Forms.RadioButton();
            this.radioImageR = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbRef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline1)).BeginInit();
            this.gbBaseline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline2)).BeginInit();
            this.gbStructure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilter)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(474, 250);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // pbRef
            // 
            this.pbRef.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.pbRef.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbRef.Location = new System.Drawing.Point(492, 12);
            this.pbRef.Name = "pbRef";
            this.pbRef.Size = new System.Drawing.Size(250, 250);
            this.pbRef.TabIndex = 2;
            this.pbRef.TabStop = false;
            // 
            // pbData
            // 
            this.pbData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.pbData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbData.Location = new System.Drawing.Point(748, 12);
            this.pbData.Name = "pbData";
            this.pbData.Size = new System.Drawing.Size(250, 250);
            this.pbData.TabIndex = 3;
            this.pbData.TabStop = false;
            // 
            // tbBaseline1
            // 
            this.tbBaseline1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBaseline1.Location = new System.Drawing.Point(6, 19);
            this.tbBaseline1.Name = "tbBaseline1";
            this.tbBaseline1.Size = new System.Drawing.Size(244, 45);
            this.tbBaseline1.TabIndex = 6;
            this.tbBaseline1.Scroll += new System.EventHandler(this.tbBaseline1_Scroll);
            // 
            // gbBaseline
            // 
            this.gbBaseline.Controls.Add(this.tbBaseline2);
            this.gbBaseline.Controls.Add(this.tbBaseline1);
            this.gbBaseline.Location = new System.Drawing.Point(748, 389);
            this.gbBaseline.Name = "gbBaseline";
            this.gbBaseline.Size = new System.Drawing.Size(256, 118);
            this.gbBaseline.TabIndex = 7;
            this.gbBaseline.TabStop = false;
            this.gbBaseline.Text = "Baseline";
            // 
            // tbBaseline2
            // 
            this.tbBaseline2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBaseline2.Location = new System.Drawing.Point(6, 70);
            this.tbBaseline2.Name = "tbBaseline2";
            this.tbBaseline2.Size = new System.Drawing.Size(244, 45);
            this.tbBaseline2.TabIndex = 7;
            this.tbBaseline2.Scroll += new System.EventHandler(this.tbBaseline2_Scroll);
            // 
            // gbStructure
            // 
            this.gbStructure.Controls.Add(this.tbStructure2);
            this.gbStructure.Controls.Add(this.tbStructure1);
            this.gbStructure.Location = new System.Drawing.Point(748, 268);
            this.gbStructure.Name = "gbStructure";
            this.gbStructure.Size = new System.Drawing.Size(250, 115);
            this.gbStructure.TabIndex = 8;
            this.gbStructure.TabStop = false;
            this.gbStructure.Text = "Structure";
            // 
            // tbStructure2
            // 
            this.tbStructure2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStructure2.Location = new System.Drawing.Point(6, 64);
            this.tbStructure2.Name = "tbStructure2";
            this.tbStructure2.Size = new System.Drawing.Size(238, 45);
            this.tbStructure2.TabIndex = 7;
            this.tbStructure2.Scroll += new System.EventHandler(this.tbStructure2_Scroll);
            // 
            // tbStructure1
            // 
            this.tbStructure1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStructure1.Location = new System.Drawing.Point(6, 19);
            this.tbStructure1.Name = "tbStructure1";
            this.tbStructure1.Size = new System.Drawing.Size(238, 45);
            this.tbStructure1.TabIndex = 6;
            this.tbStructure1.Scroll += new System.EventHandler(this.tbStructure1_Scroll);
            // 
            // scottPlotUC1
            // 
            this.scottPlotUC1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.scottPlotUC1.Location = new System.Drawing.Point(12, 265);
            this.scottPlotUC1.Name = "scottPlotUC1";
            this.scottPlotUC1.Size = new System.Drawing.Size(474, 239);
            this.scottPlotUC1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioPMT);
            this.groupBox1.Controls.Add(this.radioGoR);
            this.groupBox1.Controls.Add(this.radioDeltaGoR);
            this.groupBox1.Location = new System.Drawing.Point(492, 340);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(87, 90);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Graph";
            // 
            // radioPMT
            // 
            this.radioPMT.AutoSize = true;
            this.radioPMT.Location = new System.Drawing.Point(6, 65);
            this.radioPMT.Name = "radioPMT";
            this.radioPMT.Size = new System.Drawing.Size(48, 17);
            this.radioPMT.TabIndex = 2;
            this.radioPMT.Text = "PMT";
            this.radioPMT.UseVisualStyleBackColor = true;
            this.radioPMT.CheckedChanged += new System.EventHandler(this.radioPMT_CheckedChanged);
            // 
            // radioGoR
            // 
            this.radioGoR.AutoSize = true;
            this.radioGoR.Location = new System.Drawing.Point(6, 42);
            this.radioGoR.Name = "radioGoR";
            this.radioGoR.Size = new System.Drawing.Size(46, 17);
            this.radioGoR.TabIndex = 1;
            this.radioGoR.Text = "G/R";
            this.radioGoR.UseVisualStyleBackColor = true;
            this.radioGoR.CheckedChanged += new System.EventHandler(this.radioGoR_CheckedChanged);
            // 
            // radioDeltaGoR
            // 
            this.radioDeltaGoR.AutoSize = true;
            this.radioDeltaGoR.Checked = true;
            this.radioDeltaGoR.Location = new System.Drawing.Point(6, 19);
            this.radioDeltaGoR.Name = "radioDeltaGoR";
            this.radioDeltaGoR.Size = new System.Drawing.Size(74, 17);
            this.radioDeltaGoR.TabIndex = 0;
            this.radioDeltaGoR.TabStop = true;
            this.radioDeltaGoR.Text = "Delta G/R";
            this.radioDeltaGoR.UseVisualStyleBackColor = true;
            this.radioDeltaGoR.CheckedChanged += new System.EventHandler(this.radioDeltaGoR_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblFilterMs);
            this.groupBox2.Controls.Add(this.nudFilter);
            this.groupBox2.Location = new System.Drawing.Point(492, 441);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(87, 66);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter (px)";
            // 
            // lblFilterMs
            // 
            this.lblFilterMs.AutoSize = true;
            this.lblFilterMs.Location = new System.Drawing.Point(6, 44);
            this.lblFilterMs.Name = "lblFilterMs";
            this.lblFilterMs.Size = new System.Drawing.Size(56, 13);
            this.lblFilterMs.TabIndex = 1;
            this.lblFilterMs.Text = "123.12 ms";
            // 
            // nudFilter
            // 
            this.nudFilter.Location = new System.Drawing.Point(6, 19);
            this.nudFilter.Name = "nudFilter";
            this.nudFilter.Size = new System.Drawing.Size(74, 20);
            this.nudFilter.TabIndex = 0;
            this.nudFilter.ValueChanged += new System.EventHandler(this.nudFilter_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblPeak);
            this.groupBox3.Location = new System.Drawing.Point(585, 268);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(131, 59);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Peak dG/R";
            // 
            // lblPeak
            // 
            this.lblPeak.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPeak.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeak.Location = new System.Drawing.Point(3, 16);
            this.lblPeak.Name = "lblPeak";
            this.lblPeak.Size = new System.Drawing.Size(125, 40);
            this.lblPeak.TabIndex = 0;
            this.lblPeak.Text = "234.56%";
            this.lblPeak.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Location = new System.Drawing.Point(585, 333);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(131, 174);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Actions";
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(6, 135);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(119, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Launch Folder";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(6, 106);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(119, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Copy peak";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(6, 77);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Copy dG/R curve";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(6, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Reload";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(6, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioImageG);
            this.groupBox5.Controls.Add(this.radioImageR);
            this.groupBox5.Location = new System.Drawing.Point(492, 268);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(87, 64);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Image";
            // 
            // radioImageG
            // 
            this.radioImageG.AutoSize = true;
            this.radioImageG.Checked = true;
            this.radioImageG.Location = new System.Drawing.Point(6, 42);
            this.radioImageG.Name = "radioImageG";
            this.radioImageG.Size = new System.Drawing.Size(61, 17);
            this.radioImageG.TabIndex = 3;
            this.radioImageG.TabStop = true;
            this.radioImageG.Text = "G (Ch2)";
            this.radioImageG.UseVisualStyleBackColor = true;
            this.radioImageG.CheckedChanged += new System.EventHandler(this.radioImageG_CheckedChanged);
            // 
            // radioImageR
            // 
            this.radioImageR.AutoSize = true;
            this.radioImageR.Location = new System.Drawing.Point(6, 19);
            this.radioImageR.Name = "radioImageR";
            this.radioImageR.Size = new System.Drawing.Size(61, 17);
            this.radioImageR.TabIndex = 2;
            this.radioImageR.Text = "R (Ch1)";
            this.radioImageR.UseVisualStyleBackColor = true;
            this.radioImageR.CheckedChanged += new System.EventHandler(this.radioImageR_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 513);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbStructure);
            this.Controls.Add(this.gbBaseline);
            this.Controls.Add(this.scottPlotUC1);
            this.Controls.Add(this.pbData);
            this.Controls.Add(this.pbRef);
            this.Controls.Add(this.treeView1);
            this.Name = "FormMain";
            this.Text = "Scan-A-Gator";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbRef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline1)).EndInit();
            this.gbBaseline.ResumeLayout(false);
            this.gbBaseline.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbBaseline2)).EndInit();
            this.gbStructure.ResumeLayout(false);
            this.gbStructure.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStructure1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilter)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PictureBox pbRef;
        private System.Windows.Forms.PictureBox pbData;
        private ScottPlotDev2.ScottPlotUC scottPlotUC1;
        private System.Windows.Forms.TrackBar tbBaseline1;
        private System.Windows.Forms.GroupBox gbBaseline;
        private System.Windows.Forms.TrackBar tbBaseline2;
        private System.Windows.Forms.GroupBox gbStructure;
        private System.Windows.Forms.TrackBar tbStructure2;
        private System.Windows.Forms.TrackBar tbStructure1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioPMT;
        private System.Windows.Forms.RadioButton radioGoR;
        private System.Windows.Forms.RadioButton radioDeltaGoR;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nudFilter;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblPeak;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblFilterMs;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioImageG;
        private System.Windows.Forms.RadioButton radioImageR;
    }
}