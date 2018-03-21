namespace SSLCertificateMaker
{
	partial class PasswordPrompt
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
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.lblPasswordPrompt = new System.Windows.Forms.Label();
			this.cbMask = new System.Windows.Forms.CheckBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(16, 29);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(280, 20);
			this.txtPassword.TabIndex = 0;
			// 
			// lblPasswordPrompt
			// 
			this.lblPasswordPrompt.AutoSize = true;
			this.lblPasswordPrompt.Location = new System.Drawing.Point(13, 13);
			this.lblPasswordPrompt.Name = "lblPasswordPrompt";
			this.lblPasswordPrompt.Size = new System.Drawing.Size(143, 13);
			this.lblPasswordPrompt.TabIndex = 1;
			this.lblPasswordPrompt.Text = "Label text set in code behind";
			// 
			// cbMask
			// 
			this.cbMask.AutoSize = true;
			this.cbMask.Location = new System.Drawing.Point(16, 55);
			this.cbMask.Name = "cbMask";
			this.cbMask.Size = new System.Drawing.Size(128, 17);
			this.cbMask.TabIndex = 2;
			this.cbMask.Text = "Mask Password Input";
			this.cbMask.UseVisualStyleBackColor = true;
			this.cbMask.CheckedChanged += new System.EventHandler(this.cbMask_CheckedChanged);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(140, 88);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(221, 88);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// PasswordPrompt
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(308, 123);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.cbMask);
			this.Controls.Add(this.lblPasswordPrompt);
			this.Controls.Add(this.txtPassword);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "PasswordPrompt";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Password Prompt";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label lblPasswordPrompt;
		private System.Windows.Forms.CheckBox cbMask;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
	}
}