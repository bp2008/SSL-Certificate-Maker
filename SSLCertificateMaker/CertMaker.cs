using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Pkix;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Extension;
using Org.BouncyCastle.X509.Store;

namespace SSLCertificateMaker
{
	public static class CertMaker
	{
		internal static SecureRandom secureRandom = new SecureRandom();

		private static AsymmetricCipherKeyPair GenerateRsaKeyPair(int length)
		{
			var keygenParam = new KeyGenerationParameters(secureRandom, length);

			var keyGenerator = new RsaKeyPairGenerator();
			keyGenerator.Init(keygenParam);
			return keyGenerator.GenerateKeyPair();
		}

		private static AsymmetricCipherKeyPair GenerateEcKeyPair(string curveName)
		{
			var ecParam = SecNamedCurves.GetByName(curveName);
			var ecDomain = new ECDomainParameters(ecParam.Curve, ecParam.G, ecParam.N);
			var keygenParam = new ECKeyGenerationParameters(ecDomain, secureRandom);

			var keyGenerator = new ECKeyPairGenerator();
			keyGenerator.Init(keygenParam);
			return keyGenerator.GenerateKeyPair();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domains"></param>
		/// <param name="subjectPublic"></param>
		/// <param name="validFrom"></param>
		/// <param name="validTo"></param>
		/// <param name="issuerName"></param>
		/// <param name="issuerPublic"></param>
		/// <param name="issuerPrivate"></param>
		/// <param name="CA_PathLengthConstraint">If non-null, the certificate will be marked as a certificate authority with the specified path length constraint (0 to allow no child certificate authorities, 1 to allow 1, etc).</param>
		/// <returns></returns>
		private static X509Certificate GenerateCertificate(string[] domains, AsymmetricKeyParameter subjectPublic, DateTime validFrom, DateTime validTo, string issuerName, AsymmetricKeyParameter issuerPublic, AsymmetricKeyParameter issuerPrivate, int? CA_PathLengthConstraint)
		{
			ISignatureFactory signatureFactory;
			if (issuerPrivate is ECPrivateKeyParameters)
			{
				signatureFactory = new Asn1SignatureFactory(
					X9ObjectIdentifiers.ECDsaWithSha256.ToString(),
					issuerPrivate);
			}
			else
			{
				signatureFactory = new Asn1SignatureFactory(
					PkcsObjectIdentifiers.Sha256WithRsaEncryption.ToString(),
					issuerPrivate);
			}

			X509V3CertificateGenerator certGenerator = new X509V3CertificateGenerator();
			certGenerator.SetIssuerDN(new X509Name("CN=" + issuerName));
			certGenerator.SetSubjectDN(new X509Name("CN=" + domains[0]));
			certGenerator.SetSerialNumber(BigInteger.ProbablePrime(120, new Random()));
			certGenerator.SetNotBefore(validFrom);
			certGenerator.SetNotAfter(validTo);
			certGenerator.SetPublicKey(subjectPublic);

			if (issuerPublic != null)
			{
				AuthorityKeyIdentifierStructure akis = new AuthorityKeyIdentifierStructure(issuerPublic);
				certGenerator.AddExtension(X509Extensions.AuthorityKeyIdentifier, false, akis);
			}
			if (CA_PathLengthConstraint != null && CA_PathLengthConstraint >= 0)
			{
				X509Extension extension = new X509Extension(true, new DerOctetString(new BasicConstraints(CA_PathLengthConstraint.Value)));
				certGenerator.AddExtension(X509Extensions.BasicConstraints, extension.IsCritical, extension.GetParsedValue());
			}

			// Add SANs (Subject Alternative Names)
			GeneralName[] names = domains.Select(domain => new GeneralName(GeneralName.DnsName, domain)).ToArray();
			GeneralNames subjectAltName = new GeneralNames(names);
			certGenerator.AddExtension(X509Extensions.SubjectAlternativeName, false, subjectAltName);

			return certGenerator.Generate(signatureFactory);
		}

		private static bool ValidateCert(X509Certificate cert, ICipherParameters pubKey)
		{
			cert.CheckValidity(DateTime.UtcNow);
			var tbsCert = cert.GetTbsCertificate();
			var sig = cert.GetSignature();

			var signer = SignerUtilities.GetSigner(cert.SigAlgName);
			signer.Init(false, pubKey);
			signer.BlockUpdate(tbsCert, 0, tbsCert.Length);
			return signer.VerifySignature(sig);
		}
		private static bool IsSelfSigned(X509Certificate cert)
		{
			return ValidateCert(cert, cert.GetPublicKey());
		}

		//static IEnumerable<X509Certificate> BuildCertificateChain(X509Certificate primary, IEnumerable<X509Certificate> additional)
		//{
		//	PkixCertPathBuilder builder = new PkixCertPathBuilder();

		//	// Separate root from itermediate
		//	List<X509Certificate> intermediateCerts = new List<X509Certificate>();
		//	HashSet rootCerts = new HashSet();

		//	foreach (X509Certificate x509Cert in additional)
		//	{
		//		// Separate root and subordinate certificates
		//		if (IsSelfSigned(x509Cert))
		//			rootCerts.Add(new TrustAnchor(x509Cert, null));
		//		else
		//			intermediateCerts.Add(x509Cert);
		//	}

		//	// Create chain for this certificate
		//	X509CertStoreSelector holder = new X509CertStoreSelector();
		//	holder.Certificate = primary;

		//	// WITHOUT THIS LINE BUILDER CANNOT BEGIN BUILDING THE CHAIN
		//	intermediateCerts.Add(holder.Certificate);

		//	PkixBuilderParameters builderParams = new PkixBuilderParameters(rootCerts, holder);
		//	builderParams.IsRevocationEnabled = false;

		//	X509CollectionStoreParameters intermediateStoreParameters = new X509CollectionStoreParameters(intermediateCerts);

		//	builderParams.AddStore(X509StoreFactory.Create("Certificate/Collection", intermediateStoreParameters));

		//	PkixCertPathBuilderResult result = builder.Build(builderParams);

		//	return result.CertPath.Certificates.Cast<X509Certificate>();
		//}

		#region Public Methods
		/// <summary>
		/// Generates a self-signed certificate.  This could be used standalone or as a certificate authority.
		/// </summary>
		/// <param name="domains">An array of domain names or "subject" common names / alternative names.  If making a certificate authority, you could just let this be a single string, not even a domain name but something like "Do Not Trust This Root CA".</param>
		/// <param name="keySizeBits">Key size in bits for the RSA keys.</param>
		/// <param name="validFrom">Start date for certificate validity.</param>
		/// <param name="validTo">End date for certificate validity.</param>
		/// <returns></returns>
		public static CertificateBundle GetCertificateSignedBySelf(string[] domains, int keySizeBits, DateTime validFrom, DateTime validTo)
		{
			AsymmetricCipherKeyPair keys = GenerateRsaKeyPair(keySizeBits);
			X509Certificate cert = GenerateCertificate(domains, keys.Public, validFrom, validTo, domains[0], null, keys.Private, null);

			return new CertificateBundle(cert, keys.Private);
		}

		/// <summary>
		/// Generates a certificate signed by the specified certificate authority.
		/// </summary>
		/// <param name="domains">An array of domain names or subject common names / alternative names.</param>
		/// <param name="keySizeBits">Key size in bits for the RSA keys.</param>
		/// <param name="validFrom">Start date for certificate validity.</param>
		/// <param name="validTo">End date for certificate validity.</param>
		/// <param name="ca">A CertificateBundle representing the CA used to sign the new certificate.</param>
		/// <returns></returns>
		public static CertificateBundle GetCertificateSignedByCA(string[] domains, int keySizeBits, DateTime validFrom, DateTime validTo, CertificateBundle ca)
		{
			AsymmetricCipherKeyPair keys = GenerateRsaKeyPair(keySizeBits);
			X509Certificate cert = GenerateCertificate(domains, keys.Public, validFrom, validTo, ca.GetSubjectName(), ca.cert.GetPublicKey(), ca.privateKey, null);

			return new CertificateBundle(cert, keys.Private);
		}

		/// <summary>
		/// Generates a certificate signed by the specified certificate authority.
		/// </summary>
		/// <param name="domains">An array of domain names or subject common names / alternative names.</param>
		/// <param name="keySizeBits">Key size in bits for the RSA keys.</param>
		/// <param name="validFrom">Start date for certificate validity.</param>
		/// <param name="validTo">End date for certificate validity.</param>
		/// <param name="caName">The name of the certificate authority, e.g. "Do Not Trust This Root CA".</param>
		/// <param name="caPublic">The public key of the certificate authority.  May be null if you don't have it handy.</param>
		/// <param name="caPrivate">The private key of the certificate authority.</param>
		/// <returns></returns>
		public static CertificateBundle GetCertificateSignedByCA(string[] domains, int keySizeBits, DateTime validFrom, DateTime validTo, string caName, AsymmetricKeyParameter caPublic, AsymmetricKeyParameter caPrivate)
		{
			AsymmetricCipherKeyPair keys = GenerateRsaKeyPair(keySizeBits);
			X509Certificate cert = GenerateCertificate(domains, keys.Public, validFrom, validTo, caName, caPublic, caPrivate, null);

			return new CertificateBundle(cert, keys.Private);
		}
		#endregion
	}

	public class CertificateBundle
	{
		public X509Certificate cert;
		public AsymmetricKeyParameter privateKey;
		public CertificateBundle() { }
		public CertificateBundle(X509Certificate cert, AsymmetricKeyParameter privateKey)
		{
			this.cert = cert;
			this.privateKey = privateKey;
		}
		public string GetSubjectName()
		{
			if (cert == null)
				return "Unknown";
			string subject = cert.SubjectDN.ToString();
			if (subject.StartsWith("cn=", StringComparison.OrdinalIgnoreCase))
				subject = subject.Substring(3);
			return subject;
		}
		public byte[] GetPublicCertAsCerFile()
		{
			using (TextWriter textWriter = new StringWriter())
			{
				PemWriter pemWriter = new PemWriter(textWriter);
				pemWriter.WriteObject(cert);
				pemWriter.Writer.Flush();
				string strKey = textWriter.ToString();
				return Encoding.ASCII.GetBytes(strKey);
			}
		}
		public byte[] GetPrivateKeyAsKeyFile()
		{
			using (TextWriter textWriter = new StringWriter())
			{
				PemWriter pemWriter = new PemWriter(textWriter);
				pemWriter.WriteObject(privateKey);
				pemWriter.Writer.Flush();
				string strKey = textWriter.ToString();
				return Encoding.ASCII.GetBytes(strKey);
			}
		}
		/// <summary>
		/// Exports the certificate as a pfx file, optionally including the private key.
		/// </summary>
		/// <param name="includePrivateKey">If true, the private key is included.  If the private key is included, you should consider setting a password.</param>
		/// <param name="password">If non-null, a password is required to use the resulting pfx file.</param>
		/// <returns></returns>
		public byte[] GetPfx(bool includePrivateKey, string password)
		{
			string subject = GetSubjectName();
			Pkcs12Store pkcs12Store = new Pkcs12Store();
			X509CertificateEntry certEntry = new X509CertificateEntry(cert);
			pkcs12Store.SetCertificateEntry(subject, certEntry);
			if (includePrivateKey)
			{
				pkcs12Store.SetKeyEntry(subject, new AsymmetricKeyEntry(privateKey), new[] { certEntry });
			}
			using (MemoryStream pfxStream = new MemoryStream())
			{
				pkcs12Store.Save(pfxStream, password == null ? null : password.ToCharArray(), CertMaker.secureRandom);
				return pfxStream.ToArray();
			}
		}
		/// <summary>
		/// Loads a CertificateBundle from .cer and .key files.
		/// </summary>
		/// <param name="publicCer">The path to the public .cer file.  If null or the file does not exist, the resulting CertificateBundle will have a null [cert] field.</param>
		/// <param name="privateKey">The path to the private .key file.  If null or the file does not exist, the resulting CertificateBundle will have a null [privateKey] field.</param>
		/// <returns></returns>
		public static CertificateBundle LoadFromCerAndKeyFiles(string publicCer, string privateKey)
		{
			CertificateBundle b = new CertificateBundle();
			if (publicCer != null && File.Exists(publicCer))
			{
				using (StreamReader sr = new StreamReader(publicCer, Encoding.ASCII))
				{
					PemReader reader = new PemReader(sr);
					object obj = reader.ReadObject();
					b.cert = (X509Certificate)obj;
				}
			}
			if (privateKey != null && File.Exists(privateKey))
			{
				using (StreamReader sr = new StreamReader(privateKey, Encoding.ASCII))
				{
					PemReader reader = new PemReader(sr);
					object obj = reader.ReadObject();
					b.privateKey = ((AsymmetricCipherKeyPair)obj).Private;
				}
			}
			return b;
		}

		/// <summary>
		/// Loads a CertificateBundle from a .pfx file.
		/// </summary>
		/// <param name="filePath">The path to the .pfx file.</param>
		/// <param name="password">The password required to access the .pfx file, or null.</param>
		/// <returns></returns>
		public static CertificateBundle LoadFromPfxFile(string filePath, string password)
		{
			try
			{
				Pkcs12Store pkcs12Store = new Pkcs12Store(File.OpenRead(filePath), password == null ? null : password.ToCharArray());
				foreach (string alias in pkcs12Store.Aliases)
				{
					CertificateBundle b = new CertificateBundle();
					b.cert = pkcs12Store.GetCertificate(alias)?.Certificate;
					b.privateKey = pkcs12Store.GetKey(alias)?.Key;
					if (b.cert != null && b.privateKey != null)
						return b;
				}
				return null;
			}
			catch (IOException)
			{
				return null;
			}
		}
	}
}
