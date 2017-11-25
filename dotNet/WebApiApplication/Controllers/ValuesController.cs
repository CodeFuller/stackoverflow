using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApiApplication.Models;

namespace WebApiApplication.Controllers
{
	public sealed class SnakeCaseNamingAttribute : Attribute, IControllerConfiguration
	{
		public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
		{
			controllerSettings.Formatters.Insert(0, new SnakeCaseJsonFormatter());
		}
	}

	public class SnakeCaseJsonFormatter : JsonMediaTypeFormatter
	{
		public SnakeCaseJsonFormatter()
		{
			SerializerSettings = new JsonSerializerSettings
			{
				ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new SnakeCaseNamingStrategy()
				}
			};
		}

		public override bool CanWriteType(Type type)
		{
			return false;
		}

		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			throw new NotImplementedException();
		}
	}

	[SnakeCaseNaming]
	public class ValuesController : ApiController
	{
		// GET api/values
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		public SomeData Post(SomeData data)
		{
			return data;
		}

		// PUT api/values/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}
	}
}
