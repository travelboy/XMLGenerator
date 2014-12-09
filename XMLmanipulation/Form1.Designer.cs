namespace XMLmanipulation
{
    partial class Form1
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
            this.btnCompareTwo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowse1 = new System.Windows.Forms.Button();
            this.btnBrowse2 = new System.Windows.Forms.Button();
            this.lblFirstFile = new System.Windows.Forms.Label();
            this.lblSecondFile = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBrowse3 = new System.Windows.Forms.Button();
            this.lblThirdFile = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Label();
            this.btnLocation = new System.Windows.Forms.Button();
            this.lblLocation = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textFolderComparison = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.labelRiskcoFolder = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.labelFolderAO = new System.Windows.Forms.Label();
            this.btnMultipleFiles = new System.Windows.Forms.Button();
            this.btnDA = new System.Windows.Forms.Button();
            this.cbDatabase = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.lblDiff = new System.Windows.Forms.Label();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnCompareTwo
            // 
            this.btnCompareTwo.Location = new System.Drawing.Point(211, 287);
            this.btnCompareTwo.Name = "btnCompareTwo";
            this.btnCompareTwo.Size = new System.Drawing.Size(75, 21);
            this.btnCompareTwo.TabIndex = 0;
            this.btnCompareTwo.Text = "Compare";
            this.btnCompareTwo.UseVisualStyleBackColor = true;
            this.btnCompareTwo.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please select Riskco XML";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Please select AO XML";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnBrowse1
            // 
            this.btnBrowse1.Location = new System.Drawing.Point(211, 185);
            this.btnBrowse1.Name = "btnBrowse1";
            this.btnBrowse1.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse1.TabIndex = 3;
            this.btnBrowse1.Text = "Browse";
            this.btnBrowse1.UseVisualStyleBackColor = true;
            this.btnBrowse1.Click += new System.EventHandler(this.btnBrowse1_Click);
            // 
            // btnBrowse2
            // 
            this.btnBrowse2.Location = new System.Drawing.Point(211, 235);
            this.btnBrowse2.Name = "btnBrowse2";
            this.btnBrowse2.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse2.TabIndex = 4;
            this.btnBrowse2.Text = "Browse";
            this.btnBrowse2.UseVisualStyleBackColor = true;
            this.btnBrowse2.Click += new System.EventHandler(this.btnBrowse2_Click);
            // 
            // lblFirstFile
            // 
            this.lblFirstFile.AutoSize = true;
            this.lblFirstFile.Location = new System.Drawing.Point(16, 208);
            this.lblFirstFile.Name = "lblFirstFile";
            this.lblFirstFile.Size = new System.Drawing.Size(0, 13);
            this.lblFirstFile.TabIndex = 5;
            // 
            // lblSecondFile
            // 
            this.lblSecondFile.AutoSize = true;
            this.lblSecondFile.Location = new System.Drawing.Point(16, 253);
            this.lblSecondFile.Name = "lblSecondFile";
            this.lblSecondFile.Size = new System.Drawing.Size(0, 13);
            this.lblSecondFile.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(211, 403);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 372);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Please select XML file";
            // 
            // btnBrowse3
            // 
            this.btnBrowse3.Location = new System.Drawing.Point(211, 362);
            this.btnBrowse3.Name = "btnBrowse3";
            this.btnBrowse3.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse3.TabIndex = 9;
            this.btnBrowse3.Text = "Browse";
            this.btnBrowse3.UseVisualStyleBackColor = true;
            this.btnBrowse3.Click += new System.EventHandler(this.btnBrowse3_Click);
            // 
            // lblThirdFile
            // 
            this.lblThirdFile.AutoSize = true;
            this.lblThirdFile.Location = new System.Drawing.Point(16, 388);
            this.lblThirdFile.Name = "lblThirdFile";
            this.lblThirdFile.Size = new System.Drawing.Size(0, 13);
            this.lblThirdFile.TabIndex = 10;
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(16, 101);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(189, 13);
            this.lblOutput.TabIndex = 11;
            this.lblOutput.Text = "Please select location of the output file";
            // 
            // btnLocation
            // 
            this.btnLocation.Location = new System.Drawing.Point(211, 95);
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.Size = new System.Drawing.Size(75, 24);
            this.btnLocation.TabIndex = 12;
            this.btnLocation.Text = "Browse";
            this.btnLocation.UseVisualStyleBackColor = true;
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(16, 123);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(0, 13);
            this.lblLocation.TabIndex = 13;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 69);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(369, 20);
            this.textBox2.TabIndex = 16;
            this.textBox2.Text = "First select where you want your output file to be saved.";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(12, 152);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(369, 23);
            this.textBox3.TabIndex = 17;
            this.textBox3.Text = "............................. Compare an Riskco XML with A&O XML................." +
    ".........";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(12, 331);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(369, 20);
            this.textBox4.TabIndex = 18;
            this.textBox4.Text = ".................................... Generate single report......................" +
    ".........................";
            // 
            // textFolderComparison
            // 
            this.textFolderComparison.Location = new System.Drawing.Point(12, 469);
            this.textFolderComparison.Name = "textFolderComparison";
            this.textFolderComparison.ReadOnly = true;
            this.textFolderComparison.Size = new System.Drawing.Size(369, 20);
            this.textFolderComparison.TabIndex = 19;
            this.textFolderComparison.Text = "....................................Compare multiple XMLs........................" +
    ".................";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 519);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Please select the Riskco Folder";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(211, 519);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelRiskcoFolder
            // 
            this.labelRiskcoFolder.AutoSize = true;
            this.labelRiskcoFolder.Location = new System.Drawing.Point(28, 546);
            this.labelRiskcoFolder.Name = "labelRiskcoFolder";
            this.labelRiskcoFolder.Size = new System.Drawing.Size(0, 13);
            this.labelRiskcoFolder.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 582);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Please select the AO Folder";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(211, 577);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 24;
            this.button3.Text = "Browse";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // labelFolderAO
            // 
            this.labelFolderAO.AutoSize = true;
            this.labelFolderAO.Location = new System.Drawing.Point(19, 599);
            this.labelFolderAO.Name = "labelFolderAO";
            this.labelFolderAO.Size = new System.Drawing.Size(0, 13);
            this.labelFolderAO.TabIndex = 25;
            // 
            // btnMultipleFiles
            // 
            this.btnMultipleFiles.Location = new System.Drawing.Point(211, 772);
            this.btnMultipleFiles.Name = "btnMultipleFiles";
            this.btnMultipleFiles.Size = new System.Drawing.Size(82, 23);
            this.btnMultipleFiles.TabIndex = 26;
            this.btnMultipleFiles.Text = "Generate";
            this.btnMultipleFiles.UseVisualStyleBackColor = true;
            this.btnMultipleFiles.Click += new System.EventHandler(this.btnMultipleFiles_Click);
            // 
            // btnDA
            // 
            this.btnDA.Location = new System.Drawing.Point(299, 772);
            this.btnDA.Name = "btnDA";
            this.btnDA.Size = new System.Drawing.Size(91, 23);
            this.btnDA.TabIndex = 27;
            this.btnDA.Text = "Details Analyzer";
            this.btnDA.UseVisualStyleBackColor = true;
            this.btnDA.Click += new System.EventHandler(this.btnDA_Click);
            // 
            // cbDatabase
            // 
            this.cbDatabase.AutoSize = true;
            this.cbDatabase.Location = new System.Drawing.Point(19, 772);
            this.cbDatabase.Name = "cbDatabase";
            this.cbDatabase.Size = new System.Drawing.Size(141, 17);
            this.cbDatabase.TabIndex = 31;
            this.cbDatabase.Text = "save output to database";
            this.cbDatabase.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 664);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Please select the productpakket";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "OP",
            "IP",
            "PP",
            "WZP",
            "BOP",
            "BPP",
            "BVP",
            "GOP",
            "TPP",
            "VP",
            "VPBS"});
            this.comboBox1.Location = new System.Drawing.Point(211, 656);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(75, 21);
            this.comboBox1.TabIndex = 33;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(299, 710);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 34;
            this.button4.Text = "Browse";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // lblDiff
            // 
            this.lblDiff.AutoSize = true;
            this.lblDiff.Location = new System.Drawing.Point(16, 715);
            this.lblDiff.Name = "lblDiff";
            this.lblDiff.Size = new System.Drawing.Size(203, 13);
            this.lblDiff.TabIndex = 35;
            this.lblDiff.Text = "Select the folder with the CLCOUT results";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 807);
            this.Controls.Add(this.lblDiff);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbDatabase);
            this.Controls.Add(this.btnDA);
            this.Controls.Add(this.btnMultipleFiles);
            this.Controls.Add(this.labelFolderAO);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelRiskcoFolder);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textFolderComparison);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.btnLocation);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.lblThirdFile);
            this.Controls.Add(this.btnBrowse3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblSecondFile);
            this.Controls.Add(this.lblFirstFile);
            this.Controls.Add(this.btnBrowse2);
            this.Controls.Add(this.btnBrowse1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCompareTwo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCompareTwo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnBrowse1;
        private System.Windows.Forms.Button btnBrowse2;
        private System.Windows.Forms.Label lblFirstFile;
        private System.Windows.Forms.Label lblSecondFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBrowse3;
        private System.Windows.Forms.Label lblThirdFile;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Button btnLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textFolderComparison;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelRiskcoFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label labelFolderAO;
        private System.Windows.Forms.Button btnMultipleFiles;
        private System.Windows.Forms.Button btnDA;
        private System.Windows.Forms.CheckBox cbDatabase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label lblDiff;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
    }
}

