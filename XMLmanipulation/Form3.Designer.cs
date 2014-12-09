namespace XMLmanipulation
{
    partial class Form3
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRiskCoOut = new System.Windows.Forms.Button();
            this.btnAOOut = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblOutput = new System.Windows.Forms.Label();
            this.btnOutputO = new System.Windows.Forms.Button();
            this.lblLocationOut = new System.Windows.Forms.Label();
            this.lblRiskcoOut = new System.Windows.Forms.Label();
            this.lblAOOut = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAOClcIn = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(34, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(312, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "..........Generate differences between CLCOUT XML files.......";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(231, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Please select the Riskco Folder with clcout files";
            // 
            // btnRiskCoOut
            // 
            this.btnRiskCoOut.Location = new System.Drawing.Point(271, 159);
            this.btnRiskCoOut.Name = "btnRiskCoOut";
            this.btnRiskCoOut.Size = new System.Drawing.Size(75, 23);
            this.btnRiskCoOut.TabIndex = 22;
            this.btnRiskCoOut.Text = "Browse";
            this.btnRiskCoOut.UseVisualStyleBackColor = true;
            this.btnRiskCoOut.Click += new System.EventHandler(this.btnRiskCoOut_Click);
            // 
            // btnAOOut
            // 
            this.btnAOOut.Location = new System.Drawing.Point(271, 222);
            this.btnAOOut.Name = "btnAOOut";
            this.btnAOOut.Size = new System.Drawing.Size(75, 23);
            this.btnAOOut.TabIndex = 25;
            this.btnAOOut.Text = "Browse";
            this.btnAOOut.UseVisualStyleBackColor = true;
            this.btnAOOut.Click += new System.EventHandler(this.btnAOOut_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(34, 53);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(312, 20);
            this.textBox2.TabIndex = 26;
            this.textBox2.Text = "First select where you want your output files to be saved.";
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(31, 94);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(189, 13);
            this.lblOutput.TabIndex = 27;
            this.lblOutput.Text = "Please select location of the output file";
            // 
            // btnOutputO
            // 
            this.btnOutputO.Location = new System.Drawing.Point(271, 89);
            this.btnOutputO.Name = "btnOutputO";
            this.btnOutputO.Size = new System.Drawing.Size(75, 23);
            this.btnOutputO.TabIndex = 28;
            this.btnOutputO.Text = "Browse";
            this.btnOutputO.UseVisualStyleBackColor = true;
            this.btnOutputO.Click += new System.EventHandler(this.btnOutputO_Click);
            // 
            // lblLocationOut
            // 
            this.lblLocationOut.AutoSize = true;
            this.lblLocationOut.Location = new System.Drawing.Point(31, 129);
            this.lblLocationOut.Name = "lblLocationOut";
            this.lblLocationOut.Size = new System.Drawing.Size(0, 13);
            this.lblLocationOut.TabIndex = 29;
            // 
            // lblRiskcoOut
            // 
            this.lblRiskcoOut.AutoSize = true;
            this.lblRiskcoOut.Location = new System.Drawing.Point(34, 198);
            this.lblRiskcoOut.Name = "lblRiskcoOut";
            this.lblRiskcoOut.Size = new System.Drawing.Size(0, 13);
            this.lblRiskcoOut.TabIndex = 30;
            // 
            // lblAOOut
            // 
            this.lblAOOut.AutoSize = true;
            this.lblAOOut.Location = new System.Drawing.Point(34, 262);
            this.lblAOOut.Name = "lblAOOut";
            this.lblAOOut.Size = new System.Drawing.Size(0, 13);
            this.lblAOOut.TabIndex = 31;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(271, 436);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 32;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Please select the AO Folder with clcout files";
            // 
            // lblAOClcIn
            // 
            this.lblAOClcIn.AutoSize = true;
            this.lblAOClcIn.Location = new System.Drawing.Point(34, 331);
            this.lblAOClcIn.Name = "lblAOClcIn";
            this.lblAOClcIn.Size = new System.Drawing.Size(0, 13);
            this.lblAOClcIn.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 348);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Please select the pakket you are comparing";
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
            this.comboBox1.Location = new System.Drawing.Point(251, 345);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 37;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 489);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblAOClcIn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lblAOOut);
            this.Controls.Add(this.lblRiskcoOut);
            this.Controls.Add(this.lblLocationOut);
            this.Controls.Add(this.btnOutputO);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btnAOOut);
            this.Controls.Add(this.btnRiskCoOut);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRiskCoOut;
        private System.Windows.Forms.Button btnAOOut;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Button btnOutputO;
        private System.Windows.Forms.Label lblLocationOut;
        private System.Windows.Forms.Label lblRiskcoOut;
        private System.Windows.Forms.Label lblAOOut;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAOClcIn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
    
    }
}