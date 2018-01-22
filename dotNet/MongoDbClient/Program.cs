using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace MongoDbClient
{
	public class DummyClass
	{
		public string Name { get; set; }

		public string Type { get; set; }

		public string Date { get; set; }

		public int Age { get; set; }

		public int Value { get; set; }
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

			var database = mongoClient.GetDatabase("TestDB");
			var collection = database.GetCollection<DummyClass>("Test48359797");

			//collection.InsertOne(new DummyClass
			//{
			//	Name = "test1",
			//	Type = "typeX",
			//	Date = "10-10-10",
			//	Age = 10,
			//	Value = 20,
			//});

			//collection.InsertOne(new DummyClass
			//{
			//	Name = "test1",
			//	Type = "typeX",
			//	Date = "10-10-10",
			//	Age = 1000,
			//	Value = 2000,
			//});

			//collection.InsertOne(new DummyClass
			//{
			//	Name = "test2",
			//	Type = "typeX",
			//	Date = "10-10-10",
			//	Age = 20,
			//	Value = 30,
			//});

			//collection.InsertOne(new DummyClass
			//{
			//	Name = "test3",
			//	Type = "typeX",
			//	Date = "10-10-10",
			//	Age = 30,
			//	Value = 40,
			//});

			var date = "10-10-10";
			var type = "typeX";
			var indexNames = new List<string> { "test1", "test2" };

			var filter = new BsonDocument
			{
				{"Date", date},
				{"Type", type}
			};

			// Create the filter
			filter.Add("Name", new BsonDocument
			{
				{"$in", new BsonArray(indexNames)}
			});

			// Create the group, this does not work.
			var groupBad = new BsonDocument
			{
				{"_id",
					new BsonDocument {
						{"Name", "$Name"},
						{"Result", new BsonDocument("$push", new BsonArray()) }
					}
				}
			};

			var group = new BsonDocument
			{
				{
					"_id", new BsonDocument {
						{"Name", "$Name"},
					}
				},
				{
					"Result", new BsonDocument("$push", new BsonDocument
						{
							{ "Name", "$Name" },
							{ "Type", "$Type" },
							{ "Date", "$Date" },
							{ "Age", "$Age" },
							{ "Value", "$Value" },
						}
					)
				}
			};
			
			// Query the group with the filter
			//var resultslambda = collection.Aggregate().Match(filter).Group(group).ToList();
			var resultslambda2 = collection.Aggregate().Match(filter).Group(
				x => x.Name,
				g => new {
					Result = g.Select(x => new
					{
						x.Name,
						x.Type,
						x.Date,
						x.Age,
						x.Value
					})
				}
			).ToList();
		}
	}
}
