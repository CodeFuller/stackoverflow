using System.Net;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			var content = "Some content";
			var url = "http://localhost:18606/";

			using (WebClient client = new WebClient())
			{
				string result = client.UploadString(url, content);
			}
		}
	}
}
