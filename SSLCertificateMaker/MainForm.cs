using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace SSLCertificateMaker
{
	public partial class MainForm : Form
	{
		Thread worker;
		private const string c_SelfSigned = "*Self-Signed*";
		private const string c_make = "Make Certificate";
		private const string c_cancel = "Cancel";
		public MainForm()
		{
			InitializeComponent();
			this.Text += " " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
			dateFrom.Value = DateTime.Today.AddYears(-10);
			dateUntil.Value = DateTime.Today.AddYears(500);
			cbKeyStrength.SelectedIndex = 1;
			StopProgress();
			cbCerAndKey_CheckedChanged(null, null);
		}

		private void PopulateIssuerDropdown()
		{
			string previouslySelected = cbIssuerSelect.SelectedItem?.ToString();
			cbIssuerSelect.Items.Clear();
			cbIssuerSelect.Items.Add(c_SelfSigned);
			FileInfo exe = new FileInfo(Application.ExecutablePath);
			List<string> allCerts = new List<string>();
			foreach (FileInfo fi in exe.Directory.GetFiles())
			{
				if (string.Compare(fi.Extension, ".pfx", true) == 0 || string.Compare(fi.Extension, ".key", true) == 0)
					allCerts.Add(fi.Name);
			}
			allCerts.Sort();
			cbIssuerSelect.Items.AddRange(allCerts.ToArray());
			cbIssuerSelect.SelectedIndex = 0;
			if (previouslySelected != null)
			{
				for (int i = 0; i < cbIssuerSelect.Items.Count; i++)
					if (cbIssuerSelect.Items[i].ToString() == previouslySelected)
					{
						cbIssuerSelect.SelectedIndex = i;
						break;
					}
			}
		}

		private void btnMakeCert_Click(object sender, EventArgs e)
		{
			if (btnMakeCert.Text == c_make)
			{
				MakeCertArgs args = new MakeCertArgs(int.Parse(cbKeyStrength.SelectedItem.ToString()), dateFrom.Value, dateUntil.Value, GetCleanDomainArray(), txtCertPassword.Text, cbCerAndKey.Checked, cbIssuerSelect.SelectedItem.ToString());
				if (args.domains.Length == 0)
				{
					MessageBox.Show("No domain names were entered!" + Environment.NewLine + Environment.NewLine
						+ "You should at least enter localhost if you don't want any other domain names.");
					return;
				}
				btnMakeCert.Text = c_cancel;

				StartProgress();

				worker = new Thread(MakeCertificate);
				worker.IsBackground = true;
				worker.Name = "Make Certificate";
				worker.Start(args);
			}
			else
			{
				btnMakeCert.Enabled = false;
				worker.Abort();
			}
		}

		private string[] GetCleanDomainArray()
		{
			return txtAlternateDomains.Text.Split(new char[] { '\r', '\n' }).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
		}

		private void MakeCertificate(object Argument)
		{
			try
			{
				MakeCertArgs args = (MakeCertArgs)Argument;

				// Verify that the files do not already exist
				string safeFileName = SafeFileName(args.domains[0]);
				if (args.saveCerAndKey)
				{
					if (File.Exists(safeFileName + ".cer"))
					{
						ShowFileExistsError(safeFileName + ".cer");
						return;
					}
					else if (File.Exists(safeFileName + ".key"))
					{
						ShowFileExistsError(safeFileName + ".key");
						return;
					}
				}
				else
				{
					if (File.Exists(safeFileName + ".pfx"))
					{
						ShowFileExistsError(safeFileName + ".pfx");
						return;
					}
				}

				CertificateBundle certBundle;
				if (args.issuer == "*Self-Signed*")
				{
					certBundle = CertMaker.GetCertificateSignedBySelf(args.domains, args.keyStrength, args.validFrom, args.validTo);
				}
				else
				{
					CertificateBundle issuerBundle = null;
					if (args.issuer.EndsWith(".pfx", StringComparison.OrdinalIgnoreCase))
					{
						string password = null;
						while (issuerBundle == null)
						{
							issuerBundle = CertificateBundle.LoadFromPfxFile(args.issuer, password);
							if (issuerBundle == null)
							{
								PasswordPrompt pp = new PasswordPrompt("CA file is protected", "The .pfx file requires a password:");
								this.Invoke((Func<IWin32Window, DialogResult>)pp.ShowDialog, this);
								if (pp.OkWasClicked)
									password = pp.EnteredPassword;
								else
									return;
							}
						}
					}
					else
					{
						string cerFile = args.issuer.EndsWith(".key", StringComparison.OrdinalIgnoreCase) ? args.issuer.Remove(args.issuer.Length - 4) + ".cer" : null;
						issuerBundle = CertificateBundle.LoadFromCerAndKeyFiles(cerFile, args.issuer);
					}

					certBundle = CertMaker.GetCertificateSignedByCA(args.domains, args.keyStrength, args.validFrom, args.validTo, issuerBundle);
				}

				if (args.saveCerAndKey)
				{
					File.WriteAllBytes(safeFileName + ".cer", certBundle.GetPublicCertAsCerFile());
					File.WriteAllBytes(safeFileName + ".key", certBundle.GetPrivateKeyAsKeyFile());
				}
				else
				{
					if (args.password == "")
						args.password = null;
					File.WriteAllBytes(safeFileName + ".pfx", certBundle.GetPfx(true, args.password));
				}
			}
			catch (ThreadAbortException) { }
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			finally
			{
				StopProgress();
			}
		}

		private void ShowFileExistsError(string name)
		{
			MessageBox.Show("Unable to continue.  File already exists: " + name);
		}

		private string SafeFileName(string str)
		{
			HashSet<char> illegalChars = new HashSet<char>(new char[] { '#', '%', '&', '{', '}', '\\', '<', '>', '*', '?', '/', ' ', '$', '!', '\'', '"', ':', '@' });
			return new string(str.Select(c => illegalChars.Contains(c) ? '_' : c).ToArray());
		}
		private void StartProgress()
		{
			if (pbProgress.InvokeRequired)
				pbProgress.Invoke((Action)StartProgress);
			else
			{
				pbProgress.Style = ProgressBarStyle.Marquee;
				pbProgress.MarqueeAnimationSpeed = 30;
			}
		}
		private void StopProgress()
		{
			if (pbProgress.InvokeRequired)
				pbProgress.Invoke((Action)StopProgress);
			else
			{
				pbProgress.Style = ProgressBarStyle.Continuous;
				pbProgress.MarqueeAnimationSpeed = 0;
				btnMakeCert.Text = c_make;
				btnMakeCert.Enabled = true;
				PopulateIssuerDropdown();
			}
		}
		private class MakeCertArgs
		{
			public int keyStrength;
			public DateTime validFrom;
			public DateTime validTo;
			public string[] domains;
			public string password;
			public bool saveCerAndKey;
			public string issuer;
			public MakeCertArgs(int keyStrength, DateTime validFrom, DateTime validTo, string[] domains, string password, bool saveCerAndKey, string issuerCert)
			{
				this.keyStrength = keyStrength;
				this.validFrom = validFrom;
				this.validTo = validTo;
				this.domains = domains;
				this.password = password;
				this.saveCerAndKey = saveCerAndKey;
				this.issuer = issuerCert;
			}
		}

		private void btnConvertCerts_Click(object sender, EventArgs e)
		{
			ConvertCerts cc = new ConvertCerts();
			cc.ShowDialog(this);
			PopulateIssuerDropdown();
		}

		private void cbCerAndKey_CheckedChanged(object sender, EventArgs e)
		{
			txtCertPassword.Enabled = !cbCerAndKey.Checked;
		}
	}
}
