namespace XMLmanipulation
{
    partial class Form5
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblOutput = new System.Windows.Forms.Label();
            this.Browse = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRiskco = new System.Windows.Forms.Button();
            this.btnAOPath = new System.Windows.Forms.Button();
            this.lblRiskcoPath = new System.Windows.Forms.Label();
            this.lblAOPath = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRiskcoOut = new System.Windows.Forms.Label();
            this.lblAOOut = new System.Windows.Forms.Label();
            this.btnROut = new System.Windows.Forms.Button();
            this.btnAOut = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(18, 34);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(293, 20);
            this.textBox2.TabIndex = 18;
            this.textBox2.Text = "First select where you want your output file to be saved.";
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(18, 65);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(0, 13);
            this.lblOutput.TabIndex = 19;
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(230, 60);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(75, 23);
            this.Browse.TabIndex = 20;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Please select the Riskco CLCIN folder";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Please select the AO CLCIN folder";
            // 
            // btnRiskco
            // 
            this.btnRiskco.Location = new System.Drawing.Point(230, 99);
            this.btnRiskco.Name = "btnRiskco";
            this.btnRiskco.Size = new System.Drawing.Size(75, 23);
            this.btnRiskco.TabIndex = 27;
            this.btnRiskco.Text = "Browse";
            this.btnRiskco.UseVisualStyleBackColor = true;
            this.btnRiskco.Click += new System.EventHandler(this.btnRiskco_Click);
            // 
            // btnAOPath
            // 
            this.btnAOPath.Location = new System.Drawing.Point(230, 162);
            this.btnAOPath.Name = "btnAOPath";
            this.btnAOPath.Size = new System.Drawing.Size(75, 23);
            this.btnAOPath.TabIndex = 28;
            this.btnAOPath.Text = "Browse";
            this.btnAOPath.UseVisualStyleBackColor = true;
            this.btnAOPath.Click += new System.EventHandler(this.btnAOPath_Click);
            // 
            // lblRiskcoPath
            // 
            this.lblRiskcoPath.AutoSize = true;
            this.lblRiskcoPath.Location = new System.Drawing.Point(12, 136);
            this.lblRiskcoPath.Name = "lblRiskcoPath";
            this.lblRiskcoPath.Size = new System.Drawing.Size(0, 13);
            this.lblRiskcoPath.TabIndex = 29;
         
            // 
            // lblAOPath
            // 
            this.lblAOPath.AutoSize = true;
            this.lblAOPath.Location = new System.Drawing.Point(12, 198);
            this.lblAOPath.Name = "lblAOPath";
            this.lblAOPath.Size = new System.Drawing.Size(0, 13);
            this.lblAOPath.TabIndex = 30;
    
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(230, 370);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 31;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Please select the Riskco CLCOUT folder";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Please select the AO CLCOUT folder";
            // 
            // lblRiskcoOut
            // 
            this.lblRiskcoOut.AutoSize = true;
            this.lblRiskcoOut.Location = new System.Drawing.Point(15, 251);
            this.lblRiskcoOut.Name = "lblRiskcoOut";
            this.lblRiskcoOut.Size = new System.Drawing.Size(0, 13);
            this.lblRiskcoOut.TabIndex = 34;
           
            // 
            // lblAOOut
            // 
            this.lblAOOut.AutoSize = true;
            this.lblAOOut.Location = new System.Drawing.Point(18, 303);
            this.lblAOOut.Name = "lblAOOut";
            this.lblAOOut.Size = new System.Drawing.Size(0, 13);
            this.lblAOOut.TabIndex = 35;
    
            // 
            // btnROut
            // 
            this.btnROut.Location = new System.Drawing.Point(230, 229);
            this.btnROut.Name = "btnROut";
            this.btnROut.Size = new System.Drawing.Size(75, 23);
            this.btnROut.TabIndex = 36;
            this.btnROut.Text = "Browse";
            this.btnROut.UseVisualStyleBackColor = true;
            this.btnROut.Click += new System.EventHandler(this.btnROut_Click);
            // 
            // btnAOut
            // 
            this.btnAOut.Location = new System.Drawing.Point(230, 281);
            this.btnAOut.Name = "btnAOut";
            this.btnAOut.Size = new System.Drawing.Size(75, 23);
            this.btnAOut.TabIndex = 37;
            this.btnAOut.Text = "Browse";
            this.btnAOut.UseVisualStyleBackColor = true;
            this.btnAOut.Click += new System.EventHandler(this.btnAOut_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(51, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(245, 20);
            this.textBox1.TabIndex = 38;
            this.textBox1.Text = ".....................Mutatieeverlopen Form............................";
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 403);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnAOut);
            this.Controls.Add(this.btnROut);
            this.Controls.Add(this.lblAOOut);
            this.Controls.Add(this.lblRiskcoOut);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lblAOPath);
            this.Controls.Add(this.lblRiskcoPath);
            this.Controls.Add(this.btnAOPath);
            this.Controls.Add(this.btnRiskco);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Browse);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.textBox2);
            this.Name = "Form5";
            this.Text = "Form5";
            this.Load += new System.EventHandler(this.Form5_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRiskco;
        private System.Windows.Forms.Button btnAOPath;
        private System.Windows.Forms.Label lblRiskcoPath;
        private System.Windows.Forms.Label lblAOPath;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRiskcoOut;
        private System.Windows.Forms.Label lblAOOut;
        private System.Windows.Forms.Button btnROut;
        private System.Windows.Forms.Button btnAOut;
        private System.Windows.Forms.TextBox textBox1;
    }
}