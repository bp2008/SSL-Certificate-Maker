using System;
using Org.BouncyCastle.Asn1.X509;

namespace SSLCertificateMaker
{
	public class MakeCertArgs
	{
		public int keyStrength;
		public DateTime validFrom;
		public DateTime validTo;
		public string[] domains;
		public string password;
		public bool saveCerAndKey;
		public string issuer;
		public string OutputPath;
		public int KeyUsage;
		public KeyPurposeID[] ExtendedKeyUsage;

		public MakeCertArgs(int keyStrength, DateTime validFrom, DateTime validTo, string[] domains, string password, bool saveCerAndKey, string issuerCert, int KeyUsage, KeyPurposeID[] ExtendedKeyUsage)
		{
			this.keyStrength = keyStrength;
			this.validFrom = validFrom;
			this.validTo = validTo;
			this.domains = domains;
			this.password = password;
			this.saveCerAndKey = saveCerAndKey;
			this.issuer = issuerCert;
			this.KeyUsage = KeyUsage;
			this.ExtendedKeyUsage = ExtendedKeyUsage;
		}
	}
}
