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
		private Thread worker;

		private const string c_SelfSigned = "None (Self-Signed)";
		private const string c_make = "Make Certificate";
		private const string c_cancel = "Cancel";

		public static readonly string CA_DIR;
		public static readonly string CERT_DIR;

		static MainForm()
		{
			FileInfo exe = new FileInfo(Application.ExecutablePath);
			CA_DIR = new DirectoryInfo(Path.Combine(exe.Directory.FullName, "CA")).FullName;
			CERT_DIR = new DirectoryInfo(Path.Combine(exe.Directory.FullName, "CERT")).FullName;
			Directory.CreateDirectory(CA_DIR);
			Directory.CreateDirectory(CERT_DIR);
		}

		public MainForm()
		{
			InitializeComponent();

			this.Text += " " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
			dateFrom.Value = DateTime.Today.AddYears(-10);
			dateUntil.Value = DateTime.Today.AddYears(500);
			cbKeyStrength.SelectedIndex = 1;
			ddlOutputType.SelectedIndex = 0;
			ddlOutputType_SelectedIndexChanged(null, null);
			msKeyUsage.Initialize("Key Usage", KeyUsageOptions);
			msExtendedKeyUsage.Initialize("Extended Key Usage", ExtendedKeyUsageOptions);
			lblStatus.Text = "";

			btnPresetWebServer_Click(null, null);

			StopProgress();
		}

		private void PopulateIssuerDropdown()
		{
			string previouslySelected = cbIssuerSelect.SelectedItem?.ToString();
			cbIssuerSelect.Items.Clear();
			cbIssuerSelect.Items.Add(c_SelfSigned);
			List<string> allCerts = new List<string>();
			foreach (FileInfo fi in new DirectoryInfo(CA_DIR).GetFiles())
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
				MakeCertArgs args = new MakeCertArgs(int.Parse(cbKeyStrength.SelectedItem.ToString()),
													 dateFrom.Value,
													 dateUntil.Value,
													 GetCleanDomainArray(),
													 txtCertPassword.Text,
													 (string)ddlOutputType.SelectedItem == ".cer, .key",
													 cbIssuerSelect.SelectedItem.ToString(),
													 msKeyUsage.SelectedItems.Cast<MultiSelectListItem<int>>().Select(i => i.Value).Sum(),
													 msExtendedKeyUsage.SelectedItems.Cast<MultiSelectListItem<KeyPurposeID>>().Select(i => i.Value).ToArray()
													 );
				args.OutputPath = CERT_DIR;
				if (LooksLikeCA(args))
				{
					if (DialogResult.Yes == MessageBox.Show(
						"Should this certificate be saved in the" + Environment.NewLine
						+ "\"CA\" folder, making it available as" + Environment.NewLine
						+ "a Certificate Authority?", "Certificate Authority?", MessageBoxButtons.YesNo))
					{
						args.OutputPath = CA_DIR;
					}
				}
				if (args.domains.Length == 0)
				{
					MessageBox.Show("No domain names were entered!" + Environment.NewLine + Environment.NewLine
						+ "You should at least enter localhost if you don't want any other domain names.");
					return;
				}
				btnMakeCert.Text = c_cancel;

				StartProgress();
				SetStatus("Initializing background thread");

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

		private bool LooksLikeCA(MakeCertArgs args)
		{
			if ((args.KeyUsage & 6) != 6)
				return false;
			if (args.ExtendedKeyUsage.Length != 0)
				return false;
			return true;
		}

		private string[] GetCleanDomainArray()
		{
			return txtAlternateDomains.Text.Split(new char[] { '\r', '\n' }).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
		}

		private void MakeCertificate(object Argument)
		{
			try
			{
				Directory.CreateDirectory(CA_DIR);
				Directory.CreateDirectory(CERT_DIR);

				SetStatus("Checking for existing certificate");

				MakeCertArgs args = (MakeCertArgs)Argument;

				// Verify that the files do not already exist
				string safeFileName = Path.Combine(args.OutputPath, SafeFileName(args.domains[0]));
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

				SetStatus("Generating certificate");

				CertificateBundle certBundle;
				if (args.issuer == c_SelfSigned)
				{
					certBundle = CertMaker.GetCertificateSignedBySelf(args);
				}
				else
				{
					CertificateBundle issuerBundle = null;
					string issuerFile = Path.Combine(CA_DIR, args.issuer);
					if (issuerFile.EndsWith(".pfx", StringComparison.OrdinalIgnoreCase))
					{
						string password = null;
						while (issuerBundle == null)
						{
							issuerBundle = CertificateBundle.LoadFromPfxFile(issuerFile, password);
							if (issuerBundle == null)
							{
								PasswordPrompt pp = new PasswordPrompt("CA file is protected", "The CA private key requires a password:");
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
						string cerFile = issuerFile.EndsWith(".key", StringComparison.OrdinalIgnoreCase) ? issuerFile.Remove(issuerFile.Length - 4) + ".cer" : null;
						issuerBundle = CertificateBundle.LoadFromCerAndKeyFiles(cerFile, issuerFile);
					}

					certBundle = CertMaker.GetCertificateSignedByCA(args, issuerBundle);
				}

				SetStatus("Saving certificate to disk");

				if (args.saveCerAndKey)
				{
					File.WriteAllBytes(safeFileName + ".cer", certBundle.GetPublicCertAsCerFile());
					File.WriteAllBytes(safeFileName + ".key", certBundle.GetPrivateKeyAsKeyFile());
				}
				else
				{
					if (args.password == "")
						args.password = null;
					File.WriteAllBytes(safeFileName + ".pfx", certBundle.GetPfx(args.password));
				}

				SetStatus("");
			}
			catch (ThreadAbortException)
			{
				SetStatus("Aborted");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				SetStatus("An error occurred");
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
			if (this.InvokeRequired)
				this.Invoke((Action)StartProgress);
			else
			{
				pbProgress.Style = ProgressBarStyle.Marquee;
				pbProgress.MarqueeAnimationSpeed = 30;
			}
		}
		private void StopProgress()
		{
			if (this.InvokeRequired)
				this.Invoke((Action)StopProgress);
			else
			{
				pbProgress.Style = ProgressBarStyle.Continuous;
				pbProgress.MarqueeAnimationSpeed = 0;
				btnMakeCert.Text = c_make;
				btnMakeCert.Enabled = true;
				PopulateIssuerDropdown();
			}
		}

		private void SetStatus(string str)
		{
			if (this.InvokeRequired)
				this.Invoke((Action<string>)SetStatus, str);
			else
			{
				lblStatus.Text = str;
			}
		}
		private void ddlOutputType_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtCertPassword.Enabled = (string)ddlOutputType.SelectedItem == ".pfx";
		}
		private void cerkeyPfxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ConvertCerts cc = new ConvertCerts();
			cc.ShowDialog(this);
			PopulateIssuerDropdown();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{

		}

		#region Key Usage
		private static MultiSelectListItem<int>[] KeyUsageOptions = new MultiSelectListItem<int>[]
		{
			new MultiSelectListItem<int>("EncipherOnly (1)", KeyUsage.EncipherOnly),
			new MultiSelectListItem<int>("CRL Signing (2)", KeyUsage.CrlSign),
			new MultiSelectListItem<int>("Certificate Signing (4)", KeyUsage.KeyCertSign),
			new MultiSelectListItem<int>("KeyAgreement (8)", KeyUsage.KeyAgreement),
			new MultiSelectListItem<int>("DataEncipherment (16)", KeyUsage.DataEncipherment),
			new MultiSelectListItem<int>("KeyEncipherment (32)", KeyUsage.KeyEncipherment),
			new MultiSelectListItem<int>("NonRepudiation (64)", KeyUsage.NonRepudiation),
			new MultiSelectListItem<int>("DigitalSignature (128)", KeyUsage.DigitalSignature),
			new MultiSelectListItem<int>("DecipherOnly (32768)", KeyUsage.DecipherOnly)
		};
		#endregion

		#region Extended Key Usage
		private static MultiSelectListItem<KeyPurposeID>[] ExtendedKeyUsageOptions = new MultiSelectListItem<KeyPurposeID>[]
		{
			new MultiSelectListItem<KeyPurposeID>("Any Extended Key Usage", KeyPurposeID.AnyExtendedKeyUsage),
			new MultiSelectListItem<KeyPurposeID>("Client Auth", KeyPurposeID.IdKPClientAuth),
			new MultiSelectListItem<KeyPurposeID>("Code Signing", KeyPurposeID.IdKPCodeSigning),
			new MultiSelectListItem<KeyPurposeID>("Email Protection", KeyPurposeID.IdKPEmailProtection),
			new MultiSelectListItem<KeyPurposeID>("Ipsec End System", KeyPurposeID.IdKPIpsecEndSystem),
			new MultiSelectListItem<KeyPurposeID>("Ipsec Tunnel", KeyPurposeID.IdKPIpsecTunnel),
			new MultiSelectListItem<KeyPurposeID>("Ipsec User", KeyPurposeID.IdKPIpsecUser),
			new MultiSelectListItem<KeyPurposeID>("Mac Address", KeyPurposeID.IdKPMacAddress),
			new MultiSelectListItem<KeyPurposeID>("Ocsp Signing", KeyPurposeID.IdKPOcspSigning),
			new MultiSelectListItem<KeyPurposeID>("Server Auth", KeyPurposeID.IdKPServerAuth),
			new MultiSelectListItem<KeyPurposeID>("Smart Card Logon", KeyPurposeID.IdKPSmartCardLogon),
			new MultiSelectListItem<KeyPurposeID>("Time Stamping", KeyPurposeID.IdKPTimeStamping)
		};
		#endregion

		private void btnPresetWebServer_Click(object sender, EventArgs e)
		{
			msKeyUsage.SelectedIndices = KeyUsageOptions.Select(kvp => kvp.Key.EndsWith("(32)") || kvp.Key.EndsWith("(128)")).ToArray();
			msExtendedKeyUsage.SelectedIndices = ExtendedKeyUsageOptions.Select(kvp => kvp.Key == "Client Auth" || kvp.Key == "Server Auth").ToArray();
			cbKeyStrength.SelectedIndex = 1;

		}
		private void btnPresetCA_Click(object sender, EventArgs e)
		{
			msKeyUsage.SelectedIndices = KeyUsageOptions.Select(kvp => kvp.Key.EndsWith("(2)") || kvp.Key.EndsWith("(4)")).ToArray();
			msExtendedKeyUsage.SelectedIndices = ExtendedKeyUsageOptions.Select(kvp => false).ToArray();
			cbKeyStrength.SelectedIndex = 3;
		}
	}
}
