namespace SSLCertificateMaker
{
	partial class ConvertCerts
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConvertCerts));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cbConvertSource = new System.Windows.Forms.ComboBox();
			this.btnConvert = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.cbOutputFormat = new System.Windows.Forms.ComboBox();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(588, 89);
			this.label1.TabIndex = 0;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 105);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(155, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Choose a certificate to convert:";
			// 
			// cbConvertSource
			// 
			this.cbConvertSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbConvertSource.FormattingEnabled = true;
			this.cbConvertSource.Location = new System.Drawing.Point(187, 101);
			this.cbConvertSource.Name = "cbConvertSource";
			this.cbConvertSource.Size = new System.Drawing.Size(332, 21);
			this.cbConvertSource.TabIndex = 2;
			this.cbConvertSource.SelectedIndexChanged += new System.EventHandler(this.cbConvertSource_SelectedIndexChanged);
			// 
			// btnConvert
			// 
			this.btnConvert.Location = new System.Drawing.Point(187, 155);
			this.btnConvert.Name = "btnConvert";
			this.btnConvert.Size = new System.Drawing.Size(75, 23);
			this.btnConvert.TabIndex = 3;
			this.btnConvert.Text = "Convert";
			this.btnConvert.UseVisualStyleBackColor = true;
			this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(48, 133);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(116, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Desired Output Format:";
			// 
			// cbOutputFormat
			// 
			this.cbOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbOutputFormat.FormattingEnabled = true;
			this.cbOutputFormat.Location = new System.Drawing.Point(187, 128);
			this.cbOutputFormat.Name = "cbOutputFormat";
			this.cbOutputFormat.Size = new System.Drawing.Size(197, 21);
			this.cbOutputFormat.TabIndex = 5;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(525, 100);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(75, 23);
			this.btnRefresh.TabIndex = 6;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// ConvertCerts
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(612, 189);
			this.Controls.Add(this.btnRefresh);
			this.Controls.Add(this.cbOutputFormat);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnConvert);
			this.Controls.Add(this.cbConvertSource);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "ConvertCerts";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Convert Certificates";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbConvertSource;
		private System.Windows.Forms.Button btnConvert;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbOutputFormat;
		private System.Windows.Forms.Button btnRefresh;
	}
}