using Org.BouncyCastle.Asn1.X509;
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
	public partial class EditMultiSelect : Form
	{
		/// <summary>
		/// A boolean indicating if OK was clicked.
		/// </summary>
		public bool OkWasClicked { get; private set; } = false;
		/// <summary>
		/// The array of items available for selection.
		/// </summary>
		public readonly object[] Items;
		/// <summary>
		/// Gets an array that indicates which item indices are currently selected.
		/// </summary>
		public bool[] SelectedIndices
		{
			get
			{
				bool[] s = new bool[Items.Length];
				for (int i = 0; i < s.Length; i++)
					s[i] = listOfItems.SelectedIndices.Contains(i);
				return s;
			}
		}
		public EditMultiSelect(string title, object[] items, bool[] selectedIndices)
		{
			InitializeComponent();

			this.Text = title;
			Items = items;

			listOfItems.Items.AddRange(items);
			for (int i = 0; i < selectedIndices.Length; i++)
				if (selectedIndices[i])
					listOfItems.SelectedIndices.Add(i);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			OkWasClicked = true;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
