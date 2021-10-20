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
			this.btnMakeCert = new System.Windows.Forms.Button();
			this.dateFrom = new System.Windows.Forms.DateTimePicker();
			this.dateUntil = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtCertPassword = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.cbIssuerSelect = new System.Windows.Forms.ComboBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label11 = new System.Windows.Forms.Label();
			this.btnPresetCA = new System.Windows.Forms.Button();
			this.btnPresetWebServer = new System.Windows.Forms.Button();
			this.panel_blackline_1 = new System.Windows.Forms.Panel();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cerkeyPfxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label10 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.pbProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.ddlOutputType = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.msExtendedKeyUsage = new SSLCertificateMaker.MultiSelectCompact();
			this.msKeyUsage = new SSLCertificateMaker.MultiSelectCompact();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 89);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(127, 151);
			this.label2.TabIndex = 1;
			this.label2.Text = "Domain Names:\r\n\r\nOne per line\r\n\r\nThe first line will be the \"Subject Name\".\r\n\r\nOt" +
    "her lines will be \"Subject Alternative Names\"";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtAlternateDomains
			// 
			this.txtAlternateDomains.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAlternateDomains.Location = new System.Drawing.Point(145, 86);
			this.txtAlternateDomains.Multiline = true;
			this.txtAlternateDomains.Name = "txtAlternateDomains";
			this.txtAlternateDomains.Size = new System.Drawing.Size(331, 154);
			this.txtAlternateDomains.TabIndex = 4;
			this.txtAlternateDomains.Text = "localhost";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.Location = new System.Drawing.Point(12, 392);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(127, 13);
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
			this.cbKeyStrength.Location = new System.Drawing.Point(145, 389);
			this.cbKeyStrength.Name = "cbKeyStrength";
			this.cbKeyStrength.Size = new System.Drawing.Size(93, 21);
			this.cbKeyStrength.TabIndex = 60;
			this.toolTip1.SetToolTip(this.cbKeyStrength, "If generating a certificate to be used as a certificate authority, choose a highe" +
        "r value such as 4096.");
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(244, 392);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(207, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "bits (2048 builds fast, 4096 is more secure)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.toolTip1.SetToolTip(this.label4, "If generating a certificate to be used as a certificate authority, choose a highe" +
        "r value such as 4096.");
			// 
			// btnMakeCert
			// 
			this.btnMakeCert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnMakeCert.Location = new System.Drawing.Point(145, 487);
			this.btnMakeCert.Name = "btnMakeCert";
			this.btnMakeCert.Size = new System.Drawing.Size(133, 23);
			this.btnMakeCert.TabIndex = 90;
			this.btnMakeCert.Text = "Make Certificate";
			this.btnMakeCert.UseVisualStyleBackColor = true;
			this.btnMakeCert.Click += new System.EventHandler(this.btnMakeCert_Click);
			// 
			// dateFrom
			// 
			this.dateFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.dateFrom.Location = new System.Drawing.Point(145, 337);
			this.dateFrom.Name = "dateFrom";
			this.dateFrom.Size = new System.Drawing.Size(200, 20);
			this.dateFrom.TabIndex = 40;
			// 
			// dateUntil
			// 
			this.dateUntil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.dateUntil.Location = new System.Drawing.Point(145, 363);
			this.dateUntil.Name = "dateUntil";
			this.dateUntil.Size = new System.Drawing.Size(200, 20);
			this.dateUntil.TabIndex = 50;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Location = new System.Drawing.Point(12, 343);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(127, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Valid From:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.Location = new System.Drawing.Point(12, 369);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(127, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "Valid Until:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtCertPassword
			// 
			this.txtCertPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtCertPassword.Location = new System.Drawing.Point(145, 443);
			this.txtCertPassword.Name = "txtCertPassword";
			this.txtCertPassword.Size = new System.Drawing.Size(331, 20);
			this.txtCertPassword.TabIndex = 80;
			this.toolTip1.SetToolTip(this.txtCertPassword, "The resulting pfx file will require this password to open.");
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label6.Location = new System.Drawing.Point(12, 446);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(127, 13);
			this.label6.TabIndex = 15;
			this.label6.Text = "Pfx Password:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.toolTip1.SetToolTip(this.label6, "The resulting pfx file will require this password to open.");
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label7.Location = new System.Drawing.Point(12, 492);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(127, 13);
			this.label7.TabIndex = 16;
			this.label7.Text = "When Ready:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cbIssuerSelect
			// 
			this.cbIssuerSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbIssuerSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbIssuerSelect.FormattingEnabled = true;
			this.cbIssuerSelect.Location = new System.Drawing.Point(145, 27);
			this.cbIssuerSelect.Name = "cbIssuerSelect";
			this.cbIssuerSelect.Size = new System.Drawing.Size(331, 21);
			this.cbIssuerSelect.TabIndex = 1;
			this.toolTip1.SetToolTip(this.cbIssuerSelect, "The certificate can be self-signed or signed by another certificate in this direc" +
        "tory.");
			// 
			// label11
			// 
			this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label11.Location = new System.Drawing.Point(12, 419);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(127, 13);
			this.label11.TabIndex = 31;
			this.label11.Text = "Output Type:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.toolTip1.SetToolTip(this.label11, "The resulting pfx file will require this password to open.");
			// 
			// btnPresetCA
			// 
			this.btnPresetCA.Location = new System.Drawing.Point(234, 249);
			this.btnPresetCA.Name = "btnPresetCA";
			this.btnPresetCA.Size = new System.Drawing.Size(43, 23);
			this.btnPresetCA.TabIndex = 12;
			this.btnPresetCA.Text = "CA";
			this.toolTip1.SetToolTip(this.btnPresetCA, "Configure for a new Certificate Authority");
			this.btnPresetCA.UseVisualStyleBackColor = true;
			this.btnPresetCA.Click += new System.EventHandler(this.btnPresetCA_Click);
			// 
			// btnPresetWebServer
			// 
			this.btnPresetWebServer.Location = new System.Drawing.Point(145, 249);
			this.btnPresetWebServer.Name = "btnPresetWebServer";
			this.btnPresetWebServer.Size = new System.Drawing.Size(83, 23);
			this.btnPresetWebServer.TabIndex = 11;
			this.btnPresetWebServer.Text = "Web Server";
			this.toolTip1.SetToolTip(this.btnPresetWebServer, "Configure for a new Web Server");
			this.btnPresetWebServer.UseVisualStyleBackColor = true;
			this.btnPresetWebServer.Click += new System.EventHandler(this.btnPresetWebServer_Click);
			// 
			// panel_blackline_1
			// 
			this.panel_blackline_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel_blackline_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel_blackline_1.Location = new System.Drawing.Point(0, 54);
			this.panel_blackline_1.Name = "panel_blackline_1";
			this.panel_blackline_1.Size = new System.Drawing.Size(488, 1);
			this.panel_blackline_1.TabIndex = 23;
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(12, 30);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(127, 13);
			this.label9.TabIndex = 24;
			this.label9.Text = "Certificate Authority:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(12, 58);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(464, 20);
			this.label8.TabIndex = 25;
			this.label8.Text = "New Certificate";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(488, 24);
			this.menuStrip1.TabIndex = 26;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// convertToolStripMenuItem
			// 
			this.convertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cerkeyPfxToolStripMenuItem});
			this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
			this.convertToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.convertToolStripMenuItem.Text = "&Convert";
			// 
			// cerkeyPfxToolStripMenuItem
			// 
			this.cerkeyPfxToolStripMenuItem.Name = "cerkeyPfxToolStripMenuItem";
			this.cerkeyPfxToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.cerkeyPfxToolStripMenuItem.Text = "&cer/key <-> pfx";
			this.cerkeyPfxToolStripMenuItem.Click += new System.EventHandler(this.cerkeyPfxToolStripMenuItem_Click);
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label10.Location = new System.Drawing.Point(12, 284);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(127, 13);
			this.label10.TabIndex = 28;
			this.label10.Text = "Key Usage:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbProgress,
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 557);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(488, 22);
			this.statusStrip1.TabIndex = 29;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// pbProgress
			// 
			this.pbProgress.Name = "pbProgress";
			this.pbProgress.Size = new System.Drawing.Size(100, 16);
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(129, 17);
			this.toolStripStatusLabel1.Tag = "";
			this.toolStripStatusLabel1.Text = "status field placeholder";
			// 
			// ddlOutputType
			// 
			this.ddlOutputType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ddlOutputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlOutputType.FormattingEnabled = true;
			this.ddlOutputType.Items.AddRange(new object[] {
            ".pfx",
            ".cer, .key"});
			this.ddlOutputType.Location = new System.Drawing.Point(145, 416);
			this.ddlOutputType.Name = "ddlOutputType";
			this.ddlOutputType.Size = new System.Drawing.Size(93, 21);
			this.ddlOutputType.TabIndex = 70;
			this.ddlOutputType.SelectedIndexChanged += new System.EventHandler(this.ddlOutputType_SelectedIndexChanged);
			// 
			// label12
			// 
			this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label12.Location = new System.Drawing.Point(12, 313);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(127, 13);
			this.label12.TabIndex = 33;
			this.label12.Text = "Extended Key Usage:";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label13.Location = new System.Drawing.Point(12, 254);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(127, 13);
			this.label13.TabIndex = 35;
			this.label13.Text = "Presets:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// msExtendedKeyUsage
			// 
			this.msExtendedKeyUsage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.msExtendedKeyUsage.Items = null;
			this.msExtendedKeyUsage.Location = new System.Drawing.Point(145, 308);
			this.msExtendedKeyUsage.Name = "msExtendedKeyUsage";
			this.msExtendedKeyUsage.SelectedIndices = new bool[0];
			this.msExtendedKeyUsage.Size = new System.Drawing.Size(331, 23);
			this.msExtendedKeyUsage.TabIndex = 30;
			// 
			// msKeyUsage
			// 
			this.msKeyUsage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.msKeyUsage.Items = null;
			this.msKeyUsage.Location = new System.Drawing.Point(145, 279);
			this.msKeyUsage.Name = "msKeyUsage";
			this.msKeyUsage.SelectedIndices = new bool[0];
			this.msKeyUsage.Size = new System.Drawing.Size(331, 23);
			this.msKeyUsage.TabIndex = 20;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(488, 579);
			this.Controls.Add(this.btnPresetWebServer);
			this.Controls.Add(this.btnPresetCA);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.msExtendedKeyUsage);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.msKeyUsage);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.ddlOutputType);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.panel_blackline_1);
			this.Controls.Add(this.cbIssuerSelect);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtCertPassword);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dateUntil);
			this.Controls.Add(this.dateFrom);
			this.Controls.Add(this.btnMakeCert);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cbKeyStrength);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtAlternateDomains);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(504, 545);
			this.Name = "MainForm";
			this.Text = ".NET Certificate Authority";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtAlternateDomains;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbKeyStrength;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnMakeCert;
		private System.Windows.Forms.DateTimePicker dateFrom;
		private System.Windows.Forms.DateTimePicker dateUntil;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtCertPassword;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox cbIssuerSelect;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Panel panel_blackline_1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cerkeyPfxToolStripMenuItem;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripProgressBar pbProgress;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ComboBox ddlOutputType;
		private System.Windows.Forms.Label label11;
		private MultiSelectCompact msKeyUsage;
		private MultiSelectCompact msExtendedKeyUsage;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Button btnPresetCA;
		private System.Windows.Forms.Button btnPresetWebServer;
	}
}

