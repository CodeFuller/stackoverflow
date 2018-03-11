using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace MongoDbClient
{
	public class Student
	{
		public int Id { get; set; }

		public uint Grade { get; set; }
	}

	class Program
	{
		static IMongoCollection<Student> GetStudentCollection()
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
			return database.GetCollection<Student>("students");
		}

		static void Main(string[] args)
		{
			//UpdateDefinition<Student> updateDef = Builders<Student>.Update.Inc(x => (int)x.Grade, -1);
			//GetStudentCollection().UpdateOne(s => s.Id == 12345, updateDef);

			GetStudentCollection().UpdateOne(s => s.Id == 12345, "{ $inc: { Grade: -1 } }");
		}
	}
}
