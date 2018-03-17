using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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

			BsonClassMap.RegisterClassMap<Parameter>(cm =>
			{
				cm.AutoMap();
				cm.MapIdMember(x => x.Name);
			});

			var database = mongoClient.GetDatabase("testDB");
			var parameterCollection = database.GetCollection<Parameter>("test");

			var columns = new List<string>() {"TestColumn1"};
			var columnUnits = new Dictionary<int, string>()
			{
				{0, "SomeUnit1"}
			};

			columns.Select((columnName, index) => new Parameter
				{
					Name = columnName,
					Unit = columnUnits[index],
					ParameterType = ParameterType.TestResult
				})
				.ForEach(parameter =>
				{
					parameterCollection.ReplaceOne(Builders<Parameter>.Filter.Eq(x => x.Name, parameter.Name),
						parameter,
						new UpdateOptions
						{
							IsUpsert = true
						});
				});

			var items = parameterCollection.AsQueryable().ToList();
		}
	}
}
