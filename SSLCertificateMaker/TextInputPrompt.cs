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
	public partial class TextInputPrompt : Form
	{
		/// <summary>
		/// If true, the OK button was clicked.  If false, it was not.
		/// </summary>
		public bool OkWasClicked = false;

		/// <summary>
		/// The user-entered text.
		/// </summary>
		public string EnteredText
		{
			get
			{
				return txtInput.Text;
			}
		}

		public TextInputPrompt(string title = "Text Input Prompt", string labelText = "Enter some text:")
		{
			InitializeComponent();
			this.Text = title;
			lblTextInputPrompt.Text = labelText;
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			OkWasClicked = true;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			txtInput.Clear();
			this.Close();
		}
	}
}
