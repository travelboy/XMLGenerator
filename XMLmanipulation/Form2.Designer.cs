namespace XMLmanipulation
{
    partial class Form2
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
            this.txtAnalyzer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnRiskcoFiles = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtText = new System.Windows.Forms.TextBox();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.btnDetails = new System.Windows.Forms.Button();
            this.lblRiskcoFolder = new System.Windows.Forms.Label();
            this.lblAOFolder = new System.Windows.Forms.Label();
            this.txtXMLRiskco = new System.Windows.Forms.TextBox();
            this.txtXMLAO = new System.Windows.Forms.TextBox();
            this.lblRinfo = new System.Windows.Forms.Label();
            this.lblAinfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtAnalyzer
            // 
            this.txtAnalyzer.Location = new System.Drawing.Point(53, 12);
            this.txtAnalyzer.Name = "txtAnalyzer";
            this.txtAnalyzer.ReadOnly = true;
            this.txtAnalyzer.Size = new System.Drawing.Size(301, 20);
            this.txtAnalyzer.TabIndex = 0;
            this.txtAnalyzer.Text = "..................................... Details Analyzer..........................." +
    "......";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "select a folder of the RiskCo xmls";
            // 
            // btnRiskcoFiles
            // 
            this.btnRiskcoFiles.Location = new System.Drawing.Point(232, 51);
            this.btnRiskcoFiles.Name = "btnRiskcoFiles";
            this.btnRiskcoFiles.Size = new System.Drawing.Size(75, 23);
            this.btnRiskcoFiles.TabIndex = 2;
            this.btnRiskcoFiles.Text = "Browse";
            this.btnRiskcoFiles.UseVisualStyleBackColor = true;
            this.btnRiskcoFiles.Click += new System.EventHandler(this.btnRiskcoFiles_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "select a folder of the AO xmls";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(232, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(53, 192);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.ReadOnly = true;
            this.txtText.Size = new System.Drawing.Size(301, 36);
            this.txtText.TabIndex = 5;
            this.txtText.Text = "Please enter one line of the output file you are working with... Note that a vali" +
    "d line is one that have specified feature name in it!";
            // 
            // txtDetails
            // 
            this.txtDetails.Location = new System.Drawing.Point(53, 256);
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.Size = new System.Drawing.Size(301, 20);
            this.txtDetails.TabIndex = 6;
            // 
            // btnDetails
            // 
            this.btnDetails.Location = new System.Drawing.Point(279, 292);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(75, 23);
            this.btnDetails.TabIndex = 7;
            this.btnDetails.Text = "Get Details";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // lblRiskcoFolder
            // 
            this.lblRiskcoFolder.AutoSize = true;
            this.lblRiskcoFolder.Location = new System.Drawing.Point(53, 86);
            this.lblRiskcoFolder.Name = "lblRiskcoFolder";
            this.lblRiskcoFolder.Size = new System.Drawing.Size(0, 13);
            this.lblRiskcoFolder.TabIndex = 8;
            // 
            // lblAOFolder
            // 
            this.lblAOFolder.AutoSize = true;
            this.lblAOFolder.Location = new System.Drawing.Point(56, 160);
            this.lblAOFolder.Name = "lblAOFolder";
            this.lblAOFolder.Size = new System.Drawing.Size(0, 13);
            this.lblAOFolder.TabIndex = 9;
            // 
            // txtXMLRiskco
            // 
            this.txtXMLRiskco.Location = new System.Drawing.Point(12, 344);
            this.txtXMLRiskco.Multiline = true;
            this.txtXMLRiskco.Name = "txtXMLRiskco";
            this.txtXMLRiskco.ReadOnly = true;
            this.txtXMLRiskco.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtXMLRiskco.Size = new System.Drawing.Size(308, 266);
            this.txtXMLRiskco.TabIndex = 10;
            // 
            // txtXMLAO
            // 
            this.txtXMLAO.Location = new System.Drawing.Point(373, 344);
            this.txtXMLAO.Multiline = true;
            this.txtXMLAO.Name = "txtXMLAO";
            this.txtXMLAO.ReadOnly = true;
            this.txtXMLAO.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtXMLAO.Size = new System.Drawing.Size(301, 266);
            this.txtXMLAO.TabIndex = 12;
            // 
            // lblRinfo
            // 
            this.lblRinfo.AutoSize = true;
            this.lblRinfo.Location = new System.Drawing.Point(12, 325);
            this.lblRinfo.Name = "lblRinfo";
            this.lblRinfo.Size = new System.Drawing.Size(0, 13);
            this.lblRinfo.TabIndex = 13;
            // 
            // lblAinfo
            // 
            this.lblAinfo.AutoSize = true;
            this.lblAinfo.Location = new System.Drawing.Point(373, 325);
            this.lblAinfo.Name = "lblAinfo";
            this.lblAinfo.Size = new System.Drawing.Size(0, 13);
            this.lblAinfo.TabIndex = 14;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 622);
            this.Controls.Add(this.lblAinfo);
            this.Controls.Add(this.lblRinfo);
            this.Controls.Add(this.txtXMLAO);
            this.Controls.Add(this.txtXMLRiskco);
            this.Controls.Add(this.lblAOFolder);
            this.Controls.Add(this.lblRiskcoFolder);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.txtDetails);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRiskcoFiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAnalyzer);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAnalyzer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnRiskcoFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.TextBox txtDetails;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Label lblRiskcoFolder;
        private System.Windows.Forms.Label lblAOFolder;
        private System.Windows.Forms.TextBox txtXMLRiskco;
        private System.Windows.Forms.TextBox txtXMLAO;
        private System.Windows.Forms.Label lblRinfo;
        private System.Windows.Forms.Label lblAinfo;
    }
}