using System.Globalization;
using System.Net;
using System.Net.Http;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class Tests
	{
		[TestCase("http://xyz.example.com", "http://xyz.localhost:4000/")]
		[TestCase("http://xyz.example.com/some/inner/path", "http://xyz.localhost:4000/some/inner/path")]
		[TestCase("http://xyz.example.com/some/inner/path?param1=value1&param2=value2", "http://xyz.localhost:4000/some/inner/path?param1=value1&param2=value2")]
		[TestCase("http://example.com", "http://localhost:4000/")]
		[TestCase("http://example.com/some/inner/path", "http://localhost:4000/some/inner/path")]
		[TestCase("http://example.com/some/inner/path?param1=value1&param2=value2", "http://localhost:4000/some/inner/path?param1=value1&param2=value2")]
		public void CheckUrlRedirect(string originalUrl, string expectedRedirectUrl)
		{
			using (var httpClient = new HttpClient(new HttpClientHandler {AllowAutoRedirect = false}))
			{
				var response = httpClient.GetAsync(originalUrl).Result;

				Assert.AreEqual(HttpStatusCode.MovedPermanently, response.StatusCode);
				var redirectUrl = response.Headers.Location.ToString();
				Assert.AreEqual(expectedRedirectUrl, redirectUrl);
			}
		}
	}
}
