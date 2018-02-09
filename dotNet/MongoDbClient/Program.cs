using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace MongoDbClient
{
	internal class MyCustomDateTimeSerializer<TDateTime> : IBsonSerializer
	{
		static MyCustomDateTimeSerializer()
		{
			if (typeof(TDateTime) != typeof(DateTime) && typeof(TDateTime) != typeof(DateTime?))
			{
				throw new InvalidOperationException($"MyCustomDateTimeSerializer could be used only with {nameof(DateTime)} or {nameof(Nullable<DateTime>)}");
			}
		}
		
		public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			var data = context.Reader.ReadString();

			// Deserialization logic
			if (ValueType == typeof(DateTime?))
			{
				return null;
			}

			return new DateTime(2018, 2, 9);
		}

		public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
		{
			// Serialization logic
			throw  new NotImplementedException();
		}

		public Type ValueType => typeof(TDateTime);
	}

	public class SomeDocument
	{
		public ObjectId _id { get; set; }

		[BsonSerializer(typeof(MyCustomDateTimeSerializer<DateTime>))]
		//[BsonSerializer(typeof(MyCustomDateTimeSerializer1))]
		public DateTime Date1 { get; set; }

		//[BsonSerializer(typeof(MyCustomDateTimeSerializer2))]
		[BsonSerializer(typeof(MyCustomDateTimeSerializer<DateTime?>))]
		public DateTime? Date2 { get; set; }
	}

	class Program
	{
		static void Main(string[] args)
		{
			var mongoClient = new MongoClient(new MongoClientSettings
			{
				Server = new MongoServerAddress("localhost", 27017),
				ClusterConfigurator = cb =>
				{
					cb.Subscribe<CommandStartedEvent>(e =>
					{
						Console.WriteLine($"{e.CommandName} - {e.Command.ToJson()}");
					});
				}
			});

			var database = mongoClient.GetDatabase("testDB");
			var collection = database.GetCollection<SomeDocument>("test");
			var docs = collection.AsQueryable().ToList();
		}
	}
}
