using System.Linq;
using System.Text;
using BlowFishCS;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace ConsoleApplication
{
	class Program
	{
		static byte[] EncryptWithBlowfish()
		{
			var hashOfPrivateKey = HashValue(Encoding.ASCII.GetBytes("12345678"));
			BlowFish b = new BlowFish(hashOfPrivateKey);

			b.IV = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

			var input = Encoding.ASCII.GetBytes("12345678");
			var output = b.Encrypt_CBC(input);
			return output;
		}

		static byte[] EncryptData(byte[] input, string algorithm)
		{
			IBufferedCipher inCipher = CipherUtilities.GetCipher(algorithm);
			var hashOfPrivateKey = HashValue(Encoding.ASCII.GetBytes("12345678"));
			var key = new KeyParameter(hashOfPrivateKey);
			var IV = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
			var cipherParams = new ParametersWithIV(key, IV);
			inCipher.Init(true, cipherParams);

			return inCipher.DoFinal(input);
		}

		static void Main(string[] args)
		{
			var data = Encoding.ASCII.GetBytes("0123456789ABCDEF0123456789ABCDEF0123456789ABCDEF");
			var ctsResult = EncryptData(data, "BLOWFISH/CTS");
			var cbcResult = EncryptData(data, "BLOWFISH/CBC");
			var equalPartLength = data.Length - 2 * 8;
			var equal = ctsResult.Take(equalPartLength).SequenceEqual(cbcResult.Take(equalPartLength));
		}

		private static byte[] HashValue(byte[] v)
		{
			return v;
		}
	}
}
