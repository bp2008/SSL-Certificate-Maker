using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSLCertificateMaker
{
	// Some notes about pem files containing multiple objects: https://www.digicert.com/kb/ssl-support/pem-ssl-creation.htm
	public partial class ConvertCerts : Form
	{
		private bool suppressSourceChange = false;
		Dictionary<string, CertConversionHandler> handlers = new Dictionary<string, CertConversionHandler>();
		public ConvertCerts()
		{
			InitializeComponent();

			handlers.Add(".pfx", new CertConversionHandler(InputPfx, OutputPfx, ".pfx"));
			handlers.Add(".cer and .key", new CertConversionHandler(InputCerAndKey, OutputCerAndKey, ".cer", ".key"));

			PopulateSourceDropdown();
		}
		private void PopulateSourceDropdown()
		{
			suppressSourceChange = true;
			try
			{
				string previouslySelected = cbConvertSource.SelectedItem?.ToString();
				cbConvertSource.Items.Clear();
				List<CertItem> allCerts = new List<CertItem>();
				foreach (FileInfo fi in new DirectoryInfo(MainForm.CA_DIR).GetFiles())
				{
					if (string.Compare(fi.Extension, ".pfx", true) == 0 || string.Compare(fi.Extension, ".key", true) == 0)
						allCerts.Add(new CertItem("[CA] " + fi.Name, fi.FullName));
				}
				foreach (FileInfo fi in new DirectoryInfo(MainForm.CERT_DIR).GetFiles())
				{
					if (string.Compare(fi.Extension, ".pfx", true) == 0 || string.Compare(fi.Extension, ".key", true) == 0)
						allCerts.Add(new CertItem(fi.Name, fi.FullName));
				}
				cbConvertSource.Items.AddRange(allCerts.OrderBy(ci => ci.Name).ToArray());
				SelectPreviouslySelected(previouslySelected, cbConvertSource);
			}
			finally
			{
				suppressSourceChange = false;
				cbConvertSource_SelectedIndexChanged(null, null);
			}
		}
		private static void SelectPreviouslySelected(string previouslySelected, ComboBox cb)
		{
			if (previouslySelected != null)
			{
				for (int i = 0; i < cb.Items.Count; i++)
					if (cb.Items[i].ToString() == previouslySelected)
					{
						cb.SelectedIndex = i;
						return;
					}
			}
			if (cb.Items.Count > 0)
				cb.SelectedIndex = 0;
		}
		private CertConversionHandler FindInputHandlerForSource(string sourcePath)
		{
			foreach (KeyValuePair<string, CertConversionHandler> kvp in handlers)
			{
				if (kvp.Value.IsAllowedSource(sourcePath))
					return kvp.Value;
			}
			return null;
		}

		private void cbConvertSource_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (suppressSourceChange)
				return;
			string previouslySelected = (string)cbOutputFormat.SelectedItem;
			cbOutputFormat.Items.Clear();

			// Figure out which method IDs are allowed
			List<string> allowedOutputHandlerIDs = new List<string>();
			if (cbConvertSource.Items.Count > 0)
			{
				CertItem source = (CertItem)cbConvertSource.SelectedItem;
				foreach (KeyValuePair<string, CertConversionHandler> kvp in handlers)
				{
					if (!kvp.Value.IsAllowedSource(source.FullName))
						allowedOutputHandlerIDs.Add(kvp.Key);
				}
			}
			allowedOutputHandlerIDs.Sort();
			cbOutputFormat.Items.AddRange(allowedOutputHandlerIDs.ToArray());
			SelectPreviouslySelected(previouslySelected, cbOutputFormat);
		}


		private void btnRefresh_Click(object sender, EventArgs e)
		{
			PopulateSourceDropdown();
		}

		private void btnConvert_Click(object sender, EventArgs e)
		{
			string outputHandlerId = (string)cbOutputFormat.SelectedItem;
			if (outputHandlerId == null)
				return;
			CertItem source = (CertItem)cbConvertSource.SelectedItem;
			if (!string.IsNullOrWhiteSpace(source.FullName))
			{
				CertConversionHandler inputHandler = FindInputHandlerForSource(source.FullName);
				CertConversionHandler outputHandler = handlers[outputHandlerId];
				CertificateBundle bundle = inputHandler.ReadInput(source.FullName);
				if (bundle == null)
				{
					MessageBox.Show("Unable to read source file(s). Aborting conversion.");
				}
				else
				{
					FileInfo fiSrc = new FileInfo(source.FullName);
					string srcNoExt = fiSrc.FullName.Remove(fiSrc.FullName.Length - fiSrc.Extension.Length);
					outputHandler.WriteOutput(srcNoExt, bundle);
				}
			}
		}

		#region PFX handlers
		private CertificateBundle InputPfx(string sourcePath)
		{
			CertificateBundle bundle = null;
			string password = null;
			while (bundle == null)
			{
				bundle = CertificateBundle.LoadFromPfxFile(sourcePath, password);
				if (bundle == null)
				{
					PasswordPrompt pp = new PasswordPrompt("Enter the password", "The .pfx file requires a password:");
					pp.ShowDialog(this);
					if (pp.OkWasClicked)
						password = pp.EnteredPassword;
					else
						return null;
				}
			}
			return bundle;
		}
		private void OutputPfx(string fullNameWithoutExtension, CertificateBundle bundle)
		{
			string fileName = fullNameWithoutExtension + ".pfx";
			if (File.Exists(fileName))
			{
				DialogResult dr = MessageBox.Show("Output file \"" + fileName + "\" already exists.  Overwrite?", "Overwrite existing file?", MessageBoxButtons.YesNo);
				if (dr != DialogResult.Yes)
					return;
			}
			File.WriteAllBytes(fullNameWithoutExtension + ".pfx", bundle.GetPfx(null));
		}
		#endregion
		#region CER and KEY handlers
		private CertificateBundle InputCerAndKey(string keySourcePath)
		{
			string cerSourcePath = keySourcePath.EndsWith(".key", StringComparison.OrdinalIgnoreCase) ? keySourcePath.Remove(keySourcePath.Length - 4) + ".cer" : null;
			CertificateBundle bundle = CertificateBundle.LoadFromCerAndKeyFiles(cerSourcePath, keySourcePath);
			return bundle;
		}
		private void OutputCerAndKey(string fullNameWithoutExtension, CertificateBundle bundle)
		{
			string fileNameCer = fullNameWithoutExtension + ".cer";
			if (File.Exists(fileNameCer))
			{
				DialogResult dr = MessageBox.Show("Output file \"" + fileNameCer + "\" already exists.  Overwrite?", "Overwrite existing file?", MessageBoxButtons.YesNo);
				if (dr != DialogResult.Yes)
					return;
			}
			string fileNameKey = fullNameWithoutExtension + ".key";
			if (File.Exists(fileNameKey))
			{
				DialogResult dr = MessageBox.Show("Output file \"" + fileNameKey + "\" already exists.  Overwrite?", "Overwrite existing file?", MessageBoxButtons.YesNo);
				if (dr != DialogResult.Yes)
					return;
			}
			File.WriteAllBytes(fileNameCer, bundle.GetPublicCertAsCerFile());
			File.WriteAllBytes(fileNameKey, bundle.GetPrivateKeyAsKeyFile());
		}
		#endregion

		private class CertConversionHandler
		{
			public Func<string, CertificateBundle> ReadInput;
			public Action<string, CertificateBundle> WriteOutput;
			public string[] fileExtensions;

			public CertConversionHandler(Func<string, CertificateBundle> ReadInput, Action<string, CertificateBundle> WriteOutput, params string[] fileExtensions)
			{
				this.ReadInput = ReadInput;
				this.WriteOutput = WriteOutput;
				this.fileExtensions = fileExtensions;
			}
			public bool IsAllowedSource(string path)
			{
				foreach (string extension in fileExtensions)
				{
					if (path.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
						return true;
				}
				return false;
			}
		}

		private class CertItem
		{
			public string Name;
			public string FullName;

			public CertItem(string name, string fullName)
			{
				this.Name = name;
				this.FullName = fullName;
			}
			public override string ToString()
			{
				return Name;
			}
		}
	}
}
