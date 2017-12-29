using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace MongoDbClient
{
	public class TestDocument : Document
	{
		public int NumericData { get; set; }

		public string StringData { get; set; }
	}

	class Program
	{
		static void Main(string[] args)
		{
			var document = new TestDocument
			{
				Id = 123,
				NumericData = 456,
				StringData = "Initial data",
			};

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

			var repository = new Repository<TestDocument>(mongoClient, "TestDB", "TestDocuments");

			repository.AddDocument(document);

			document.StringData = "Second data";
			repository.UpdateDocument(document);

			document.StringData = "Third data";
			repository.UpsertDocument(document);

			var documents = repository.GetDocuments().ToList();

			repository.DeleteDocument(document.Id);

			document.StringData = "Fourth data";
			repository.UpsertDocument(document);
		}
	}
}
