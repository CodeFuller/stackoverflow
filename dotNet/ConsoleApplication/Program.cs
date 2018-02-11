using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var httpClient = new HttpClient())
			{
				httpClient.BaseAddress = new Uri("http://localhost:12345/api/");

				var authResponse = httpClient.GetAsync("auth").Result;
				if (!authResponse.IsSuccessStatusCode)
				{
					Console.WriteLine($"Failed to authenticate: {authResponse.StatusCode}");
					return;
				}

				var jwtToken = authResponse.Content.ReadAsStringAsync().Result;
				Console.WriteLine($"JWT = {jwtToken}");

				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

				var apiResponse = httpClient.GetAsync("values").Result;
				if (apiResponse.StatusCode == HttpStatusCode.Unauthorized)
				{
					Console.WriteLine("FAILED :(");
				}
				else
				{
					Console.WriteLine($"SUCCEEDED: {apiResponse.StatusCode} !!!");
				}
			}
		}
	}
}
