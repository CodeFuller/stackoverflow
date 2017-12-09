using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetCore.WebApiApplication.Controllers
{
	public class OptionalValue<T>
	{
		private T value;
		public T Value
		{
			get => value;

			set
			{
				HasValue = true;
				this.value = value;
			}
		}

		public bool HasValue { get; set; }
	}

	class OptionalValueConverter<T> : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(OptionalValue<T>);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return new OptionalValue<T>
			{
				Value = (T) reader.Value,
			};
		}

		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}

	public class SomeModel
	{
		public string Surname { get; set; }

		[JsonConverter(typeof(OptionalValueConverter<string>))]
		public OptionalValue<string> Email { get; set; } = new OptionalValue<string>();
	}

	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		// GET api/values
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody]string value)
		{
		}

		// POST api/values
		[HttpPatch]
		public void Patch([FromBody]SomeModel data)
		{
			if (data.Email.HasValue)
			{
				//	Email presents in Json
				if (data.Email.Value == null)
				{
					//	Email should be removed
				}
				else
				{
					//	Email should be updated
				}
			}
			else
			{
				//	Email does not present in Json and should not be affected
			}
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
