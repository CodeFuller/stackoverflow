using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication
{
	public static class ObjectExtensions
	{
		public static IDictionary<string, string> ToKeyValue(this object metaToken)
		{
			if (metaToken == null)
			{
				return null;
			}

			JToken token = metaToken as JToken;
			if (token == null)
			{
				return ToKeyValue(JObject.FromObject(metaToken));
			}

			if (token.HasValues)
			{
				var contentData = new Dictionary<string, string>();
				foreach (var child in token.Children().ToList())
				{
					var childContent = child.ToKeyValue();
					if (childContent != null)
					{
						contentData = contentData.Concat(childContent)
							.ToDictionary(k => k.Key, v => v.Value);
					}
				}

				return contentData;
			}

			var jValue = token as JValue;
			if (jValue?.Value == null)
			{
				return null;
			}

			var value = jValue?.Type == JTokenType.Date ?
				jValue?.ToString("o", CultureInfo.InvariantCulture) :
				jValue?.ToString(CultureInfo.InvariantCulture);

			return new Dictionary<string, string> { { token.Path, value } };
		}
	}

	class SaveProfileRequest
	{
		public string gName { get; set; }
		public string gEmail { get; set; }
		public long gContact { get; set; }
		public string gCompany { get; set; }
		public string gDeviceID { get; set; }
		public string Organization { get; set; }
		public string profileImage { get; set; }
		public string documentImagefront { get; set; }
		public string documentImageback { get; set; }
	}

	class Program
	{
		static void Main(string[] args)
		{
			var data = new
			{
				gName = "Some Name",
				gEmail = "some@email.com",
			};

			var keyValues = data.ToKeyValue();
			var content = new FormUrlEncodedContent(data.ToKeyValue());
		}
	}
}
