namespace XMLmanipulation
{
    partial class Form4
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
            this.btnOutput = new System.Windows.Forms.Button();
            this.lblWhere = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnFolder = new System.Windows.Forms.Button();
            this.lblFolder = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAO = new System.Windows.Forms.Button();
            this.lblorsima = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 58);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(369, 20);
            this.textBox2.TabIndex = 17;
            this.textBox2.Text = "First select where you want your output file to be saved.";
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(24, 102);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(189, 13);
            this.lblOutput.TabIndex = 18;
            this.lblOutput.Text = "Please select location of the output file";
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(236, 102);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(75, 23);
            this.btnOutput.TabIndex = 19;
            this.btnOutput.Text = "Browse";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // lblWhere
            // 
            this.lblWhere.AutoSize = true;
            this.lblWhere.Location = new System.Drawing.Point(24, 126);
            this.lblWhere.Name = "lblWhere";
            this.lblWhere.Size = new System.Drawing.Size(0, 13);
            this.lblWhere.TabIndex = 20;
          
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Please select the Riskco folder";
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(236, 163);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(75, 23);
            this.btnFolder.TabIndex = 22;
            this.btnFolder.Text = "Browse";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(24, 184);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(0, 13);
            this.lblFolder.TabIndex = 23;
      
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(236, 302);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 24;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Please select the AO folder";
            // 
            // btnAO
            // 
            this.btnAO.Location = new System.Drawing.Point(236, 220);
            this.btnAO.Name = "btnAO";
            this.btnAO.Size = new System.Drawing.Size(75, 23);
            this.btnAO.TabIndex = 26;
            this.btnAO.Text = "Browse";
            this.btnAO.UseVisualStyleBackColor = true;
            this.btnAO.Click += new System.EventHandler(this.btnAO_Click);
            // 
            // lblorsima
            // 
            this.lblorsima.AutoSize = true;
            this.lblorsima.Location = new System.Drawing.Point(24, 238);
            this.lblorsima.Name = "lblorsima";
            this.lblorsima.Size = new System.Drawing.Size(0, 13);
            this.lblorsima.TabIndex = 27;
   
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(101, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(174, 20);
            this.textBox1.TabIndex = 28;
            this.textBox1.Text = "................PP/WZP Form...............";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 375);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblorsima);
            this.Controls.Add(this.btnAO);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblWhere);
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.textBox2);
            this.Name = "Form4";
            this.Text = "Form4";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Label lblWhere;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAO;
        private System.Windows.Forms.Label lblorsima;
        private System.Windows.Forms.TextBox textBox1;
    }
}