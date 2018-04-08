Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates

Module MainModule

    Sub Main()

	    Dim tStamp As String = CInt((DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString
	    Dim ampBaseUrl = "https://www-toptrouwen-nl.cdn.ampproject.org"
	    Dim signatureUrl As String = "/update-cache/c/s/www.toptrouwen.nl/artikelen/132/het-uitwisselen-van-de-trouwringen-hoe-voorkom-je-bloopers?amp_action=flush&amp_ts=" + tStamp

	    Dim data() As Byte = System.Text.Encoding.ASCII.GetBytes(signatureUrl)

	    Dim certificate = New X509Certificate2("d:\temp\keys\keys.pfx", "12345")
	    Dim rsa As RSA = certificate.GetRSAPrivateKey()
	    Dim sig() As Byte = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1)

	    Dim ampUrlSignature As String = ToWebSafeBase64(sig)
	    Dim url As String = ampBaseUrl + signatureUrl + "&amp_url_signature=" + ampUrlSignature

        Console.WriteLine(url)

    End Sub

    Private Function ToWebSafeBase64([data]() As Byte) As String
        Dim base64 = System.Convert.ToBase64String(data)
        base64 = base64.Replace("+", "-")
        base64 = base64.Replace("/", "_")
	    Return base64
    End Function

End Module
