namespace SSLCertificateMaker
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			this.label2 = new System.Windows.Forms.Label();
			this.txtAlternateDomains = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cbKeyStrength = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.pbProgress = new System.Windows.Forms.ProgressBar();
			this.btnMakeCert = new System.Windows.Forms.Button();
			this.dateFrom = new System.Windows.Forms.DateTimePicker();
			this.dateUntil = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtCertPassword = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lblStatus = new System.Windows.Forms.Label();
			this.cbIssuerSelect = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.cbCerAndKey = new System.Windows.Forms.CheckBox();
			this.btnConvertCerts = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(127, 181);
			this.label2.TabIndex = 1;
			this.label2.Text = "Domain Names:\r\n\r\nOne per line\r\n\r\nThe first line will be the \"Subject Name\" and \"I" +
    "ssuer Name\".\r\n\r\nOther lines will be \"Subject Alternative Names\"";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtAlternateDomains
			// 
			this.txtAlternateDomains.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAlternateDomains.Location = new System.Drawing.Point(145, 39);
			this.txtAlternateDomains.Multiline = true;
			this.txtAlternateDomains.Name = "txtAlternateDomains";
			this.txtAlternateDomains.Size = new System.Drawing.Size(330, 154);
			this.txtAlternateDomains.TabIndex = 1;
			this.txtAlternateDomains.Text = "localhost";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(68, 254);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Key Strength:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.toolTip1.SetToolTip(this.label3, "If generating a certificate to be used as a certificate authority, choose a highe" +
        "r value such as 4096.");
			// 
			// cbKeyStrength
			// 
			this.cbKeyStrength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbKeyStrength.DisplayMember = "2048";
			this.cbKeyStrength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbKeyStrength.FormattingEnabled = true;
			this.cbKeyStrength.Items.AddRange(new object[] {
            "1024",
            "2048",
            "3072",
            "4096",
            "8192",
            "16384"});
			this.cbKeyStrength.Location = new System.Drawing.Point(145, 251);
			this.cbKeyStrength.Name = "cbKeyStrength";
			this.cbKeyStrength.Size = new System.Drawing.Size(93, 21);
			this.cbKeyStrength.TabIndex = 4;
			this.toolTip1.SetToolTip(this.cbKeyStrength, "If generating a certificate to be used as a certificate authority, choose a highe" +
        "r value such as 4096.");
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(244, 254);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(207, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "bits (2048 builds fast, 4096 is more secure)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.toolTip1.SetToolTip(this.label4, "If generating a certificate to be used as a certificate authority, choose a highe" +
        "r value such as 4096.");
			// 
			// pbProgress
			// 
			this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pbProgress.Location = new System.Drawing.Point(284, 311);
			this.pbProgress.Name = "pbProgress";
			this.pbProgress.Size = new System.Drawing.Size(191, 23);
			this.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.pbProgress.TabIndex = 10;
			// 
			// btnMakeCert
			// 
			this.btnMakeCert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnMakeCert.Location = new System.Drawing.Point(145, 311);
			this.btnMakeCert.Name = "btnMakeCert";
			this.btnMakeCert.Size = new System.Drawing.Size(133, 23);
			this.btnMakeCert.TabIndex = 8;
			this.btnMakeCert.Text = "Make Certificate";
			this.btnMakeCert.UseVisualStyleBackColor = true;
			this.btnMakeCert.Click += new System.EventHandler(this.btnMakeCert_Click);
			// 
			// dateFrom
			// 
			this.dateFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.dateFrom.Location = new System.Drawing.Point(145, 199);
			this.dateFrom.Name = "dateFrom";
			this.dateFrom.Size = new System.Drawing.Size(200, 20);
			this.dateFrom.TabIndex = 2;
			// 
			// dateUntil
			// 
			this.dateUntil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.dateUntil.Location = new System.Drawing.Point(145, 225);
			this.dateUntil.Name = "dateUntil";
			this.dateUntil.Size = new System.Drawing.Size(200, 20);
			this.dateUntil.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(80, 202);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Valid From:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(82, 228);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(57, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "Valid Until:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtCertPassword
			// 
			this.txtCertPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtCertPassword.Location = new System.Drawing.Point(145, 278);
			this.txtCertPassword.Name = "txtCertPassword";
			this.txtCertPassword.Size = new System.Drawing.Size(330, 20);
			this.txtCertPassword.TabIndex = 5;
			this.toolTip1.SetToolTip(this.txtCertPassword, "The resulting pfx file will require this password to open.");
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(65, 281);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(74, 13);
			this.label6.TabIndex = 15;
			this.label6.Text = "Pfx Password:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.toolTip1.SetToolTip(this.label6, "The resulting pfx file will require this password to open.");
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(65, 316);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73, 13);
			this.label7.TabIndex = 16;
			this.label7.Text = "When Ready:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblStatus
			// 
			this.lblStatus.Location = new System.Drawing.Point(15, 342);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(124, 23);
			this.lblStatus.TabIndex = 18;
			// 
			// cbIssuerSelect
			// 
			this.cbIssuerSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbIssuerSelect.FormattingEnabled = true;
			this.cbIssuerSelect.Location = new System.Drawing.Point(145, 12);
			this.cbIssuerSelect.Name = "cbIssuerSelect";
			this.cbIssuerSelect.Size = new System.Drawing.Size(330, 21);
			this.cbIssuerSelect.TabIndex = 19;
			this.toolTip1.SetToolTip(this.cbIssuerSelect, "The certificate can be self-signed or signed by another certificate in this direc" +
        "tory.");
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(76, 15);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(63, 13);
			this.label8.TabIndex = 20;
			this.label8.Text = "Issuer / CA:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.toolTip1.SetToolTip(this.label8, "The certificate can be self-signed or signed by another certificate in this direc" +
        "tory.");
			// 
			// cbCerAndKey
			// 
			this.cbCerAndKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbCerAndKey.AutoSize = true;
			this.cbCerAndKey.Location = new System.Drawing.Point(145, 342);
			this.cbCerAndKey.Name = "cbCerAndKey";
			this.cbCerAndKey.Size = new System.Drawing.Size(205, 17);
			this.cbCerAndKey.TabIndex = 21;
			this.cbCerAndKey.Text = "Save as .cer and .key (instead of .pfx)";
			this.cbCerAndKey.UseVisualStyleBackColor = true;
			this.cbCerAndKey.CheckedChanged += new System.EventHandler(this.cbCerAndKey_CheckedChanged);
			// 
			// btnConvertCerts
			// 
			this.btnConvertCerts.Location = new System.Drawing.Point(317, 365);
			this.btnConvertCerts.Name = "btnConvertCerts";
			this.btnConvertCerts.Size = new System.Drawing.Size(158, 23);
			this.btnConvertCerts.TabIndex = 22;
			this.btnConvertCerts.Text = "Convert cer/key <-> pfx";
			this.btnConvertCerts.UseVisualStyleBackColor = true;
			this.btnConvertCerts.Click += new System.EventHandler(this.btnConvertCerts_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(487, 395);
			this.Controls.Add(this.btnConvertCerts);
			this.Controls.Add(this.cbCerAndKey);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.cbIssuerSelect);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtCertPassword);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dateUntil);
			this.Controls.Add(this.dateFrom);
			this.Controls.Add(this.btnMakeCert);
			this.Controls.Add(this.pbProgress);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cbKeyStrength);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtAlternateDomains);
			this.Controls.Add(this.label2);
			this.Name = "MainForm";
			this.Text = "Self-Signed Certificate Maker";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtAlternateDomains;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbKeyStrength;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ProgressBar pbProgress;
		private System.Windows.Forms.Button btnMakeCert;
		private System.Windows.Forms.DateTimePicker dateFrom;
		private System.Windows.Forms.DateTimePicker dateUntil;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtCertPassword;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.ComboBox cbIssuerSelect;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.CheckBox cbCerAndKey;
		private System.Windows.Forms.Button btnConvertCerts;
	}
}

