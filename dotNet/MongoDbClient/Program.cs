using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace MongoDbClient
{
	public class SomeMongoDocument
	{
		public ObjectId _id { get; set; }

		public string FileName { get; set; }

		public BsonDocument Metadata { get; set; }
	}

	class Program
	{
		private IMongoCollection<SomeMongoDocument> MongoCollection { get; set; }

		static void Main(string[] args)
		{
			Program app = new Program();
			app.Run();
		}

		private void Run()
		{
			var filename = "SomeFilename";
			var metadata = "{ \"field1\": \"NewValue1\", \"field2\": \"value2\" }";

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
			MongoCollection = database.GetCollection<SomeMongoDocument>("metadata");

			var filter = Builders<SomeMongoDocument>.Filter.Eq(e => e.FileName, filename);

			BsonDocument document = BsonSerializer.Deserialize<BsonDocument>(metadata);

			UpdateDefinition<SomeMongoDocument> update = null;
			foreach (BsonElement element in document)
			{
				update = update?.Set(e => e.Metadata[element.Name], element.Value) ??
							Builders<SomeMongoDocument>.Update.Set(e => e.Metadata[element.Name], element.Value);
			}

			if (update != null)
			{
				this.MongoCollection.UpdateOne(filter, update);
			}
		}
	}
}
