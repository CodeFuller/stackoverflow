using Newtonsoft.Json;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			var serializationSettings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Objects
			};

			var json = JsonConvert.SerializeObject(new OldType { Data = "Some data" }, serializationSettings);

			var deserializationSettings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.All,
				SerializationBinder = new ReplaceOldTypesBinder(),
			};

			var deserialized = JsonConvert.DeserializeObject(json, deserializationSettings);
		}
	}
}
