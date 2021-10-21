using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSLCertificateMaker
{
	/// <summary>
	/// Identifies the relationships between multiple certificates.
	/// </summary>
	public static class ChainBuilder
	{
		/// <summary>
		/// Returns the full certificate chain correctly ordered (starting with [cert], each following array element is the issuer of the element before it). If the input chain is broken, the output chain will not include all certificates.
		/// </summary>
		/// <param name="cert">The primary certificate.</param>
		/// <param name="unorderedChain">Unordered certificates making up the chain of trust. Ideally this should include the root CA and all intermediate CAs.</param>
		/// <returns></returns>
		public static X509Certificate[] BuildChain(X509Certificate cert, IEnumerable<X509Certificate> unorderedChain)
		{
			ChainLink root = new ChainLink(cert);
			IEnumerable<ChainLink> allLinks = unorderedChain
				.Select(c => new ChainLink(c))
				.Concat(new ChainLink[] { root }).ToArray();

			Dictionary<string, ChainLink> uidMap = allLinks.ToDictionary(c => c.cert.SubjectDN.ToString());

			foreach (ChainLink l in allLinks)
			{
				if (uidMap.TryGetValue(l.cert.IssuerDN.ToString(), out ChainLink issuer))
				{
					l.issuer = issuer;
				}
			}

			List<X509Certificate> chain = new List<X509Certificate>();
			while (root != null)
			{
				chain.Add(root.cert);
				if (root.issuer == root)
					break;
				root = root.issuer;
			}
			return chain.ToArray();
		}
		private class ChainLink
		{
			public X509Certificate cert;
			public ChainLink issuer;
			public ChainLink(X509Certificate cert)
			{
				this.cert = cert;
			}
			public override string ToString()
			{
				return cert.SubjectDN.ToString() + " (issued by " + cert.IssuerDN?.ToString() + ")" + (cert.IssuerDN != null && issuer == null ? " [issuer prop not set]" : "");
			}
		}
	}
}
