Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates

Module MainModule

	Sub Main()

		Dim signatureUrl = "Test Url"
		Dim data() As Byte = System.Text.Encoding.ASCII.GetBytes(signatureUrl)

		Dim certificate = New X509Certificate2("d:\temp\keys\keys.pfx", "12345")
		Dim rsa As RSA = certificate.GetRSAPrivateKey()
		Dim sig() As Byte = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1)

	End Sub

End Module
