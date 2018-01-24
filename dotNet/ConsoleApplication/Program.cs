using System;
using System.Net.Http;
using System.Net.Http.Headers;
using ConsoleApplication.Dal;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var client = new HttpClient())
			{
				var ApiBaseUrl = "http://localhost:19081/";
				long SPId = 123;
				var ApiActionName = "BridgeSP";

				//client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", GenerateAuthenticationToken());
				client.BaseAddress = new Uri(ApiBaseUrl);

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				//HTTP POST
				var responseTask = client.PostAsync(ApiActionName + "/" + SPId, null);
				responseTask.Wait();

				var result = responseTask.Result;
				//	This will throw if request finished with some HTTP error
				result.EnsureSuccessStatusCode();

				var responseValue = result.Content.ReadAsAsync<string>().Result;
				var response = result.StatusCode + "|" + responseValue;
			}
		}
	}
}
