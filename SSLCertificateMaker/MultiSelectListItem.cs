using Org.BouncyCastle.Asn1.X509;

namespace SSLCertificateMaker
{
	public class MultiSelectListItem<T>
	{
		public readonly string Key;
		public readonly T Value;
		public MultiSelectListItem()
		{
		}
		public MultiSelectListItem(string key, T value)
		{
			Key = key;
			Value = value;
		}
		public override string ToString()
		{
			return Key;
		}
	}
}
