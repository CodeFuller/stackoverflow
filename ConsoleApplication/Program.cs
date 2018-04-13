using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			byte[] requestToSign = new byte[1024];
			var _certificatePath = @"d:\temp\keys\privatekey.pem";

			using (var txtreader = new StringReader(File.ReadAllText(_certificatePath)))
			{
				var keyPair = (AsymmetricCipherKeyPair)new PemReader(txtreader).ReadObject();

				var key = keyPair.Private as RsaPrivateCrtKeyParameters;
				ISigner sig = SignerUtilities.GetSigner("SHA512withRSA");
				sig.Init(true, key);

				sig.BlockUpdate(requestToSign, 0, requestToSign.Length);
				byte[] signature = sig.GenerateSignature();
			}
		}
	}
}
