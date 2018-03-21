using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSLCertificateMaker
{
	public partial class PasswordPrompt : Form
	{
		/// <summary>
		/// If true, the OK button was clicked.  If false, it was not.
		/// </summary>
		public bool OkWasClicked = false;
		/// <summary>
		/// The user-entered password.
		/// </summary>
		public string EnteredPassword
		{
			get
			{
				return txtPassword.Text;
			}
		}
		public PasswordPrompt(string title = "Password Prompt", string labelText = "Enter the password:")
		{
			InitializeComponent();
			this.Text = title;
			lblPasswordPrompt.Text = labelText;
			cbMask_CheckedChanged(null, null);
		}

		private void cbMask_CheckedChanged(object sender, EventArgs e)
		{
			txtPassword.UseSystemPasswordChar = cbMask.Checked;
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			OkWasClicked = true;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			txtPassword.Clear();
			this.Close();
		}
	}
}
