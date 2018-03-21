using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace MongoDbClient
{

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

			var pack = new ConventionPack
			{
				new IgnoreDefaultPropertiesConvention<SomeDocument>()
			};
			ConventionRegistry.Register("Custom Conventions", pack, t => true);

			var database = mongoClient.GetDatabase("testDB");
			var collection = database.GetCollection<SomeDocument>("testCollection");

			collection.InsertOne(new SomeDocument());
			collection.InsertOne(new SomeDocument
			{
				StringData = "Test data",
				NumericData = 12345,
			});
		}
	}
}
