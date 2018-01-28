using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			var field1 = "value1";
			var field2 = "value2";
			var field3 = "value3";
			var field4 = "value4";
			var Baseurl = "http://localhost:19081/";

			using (var client = new HttpClient())
			{
				//set up client
				client.BaseAddress = new Uri(Baseurl);
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var requestUri = new Uri($"api/home/processfields?field1={HttpUtility.UrlEncode(field1)}&field2={HttpUtility.UrlEncode(field2)}&field4={HttpUtility.UrlEncode(field4)}", UriKind.Relative);

				Console.WriteLine("Requesting...");
				HttpResponseMessage Res = client.PostAsync(requestUri, new StringContent($"\"{field3}\"", Encoding.UTF8, "application/json")).Result;
				Console.WriteLine($"Result: {Res.StatusCode}");
			}
		}
	}
}
