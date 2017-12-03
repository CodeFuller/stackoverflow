using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ConsoleApplication.Dal;
using Newtonsoft.Json;

namespace ConsoleApplication
{
	class Program
	{
		public async Task<T> PutObject<T>(string path, T content, string accessToken)
		{
			using (var httpClient = new HttpClient())
			{
				try
				{
					SetBaseUri(httpClient, accessToken);

					var serialisezContent = CreateHttpContent(content);

					var httpResponse = await httpClient.PutAsync(path, serialisezContent);

					if (httpResponse.StatusCode == HttpStatusCode.InternalServerError) throw new Exception("Problem accessing the api");

					//return JsonConvert.DeserializeObject<T>(GetResult(httpResponse));

					throw new NotImplementedException();
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}

		}

		private void SetBaseUri(HttpClient httpClient, string accessToken)
		{
			httpClient.BaseAddress = new Uri(BaseUri);
			//	CF TEMP
			//httpClient.DefaultRequestHeaders.Authorization = _authenticationHeaderValueCreator.CreateAuthenticationHeaderValue("bearer", accessToken);
		}

		public ByteArrayContent CreateHttpContent<TParam>(TParam httpObject)
		{
			var content = JsonConvert.SerializeObject(httpObject);
			var buffer = System.Text.Encoding.UTF8.GetBytes(content);
			var byteContent = new ByteArrayContent(buffer);

			byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return byteContent;
		}

		static void Main(string[] args)
		{
			new MusicLibraryRepository().Test();
		}
	}
}
