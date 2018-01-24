using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace MongoDbClient
{
	public class Author
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public IList<Book> Books { get; set; }
		public bool Deleted { get; set; }
	}

	public class Book
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public bool Deleted { get; set; }
	}

	class Program
	{
		static void Main(string[] args)
		{
			var book1 = new Book
			{
				Id = "1",
				Name = "Voina i Mir",
				Deleted = false,
			};

			var book2 = new Book
			{
				Id = "2",
				Name = "Anna Karenina",
				Deleted = true,
			};

			var book3 = new Book
			{
				Id = "3",
				Name = "Dead Souls",
				Deleted = false,
			};

			var book4 = new Book
			{
				Id = "4",
				Name = "Pugachev",
				Deleted = true,
			};

			var author1 = new Author
			{
				Id = "1",
				Name = "Tolstoy",
				Books = new List<Book> { book1, book2 },
				Deleted = false,
			};

			var author2 = new Author
			{
				Id = "2",
				Name = "Pushkin",
				Books = new List<Book> { book3 },
				Deleted = true,
			};

			var author3 = new Author
			{
				Id = "3",
				Name = "Gogol",
				Books = new List<Book> { book4 },
				Deleted = false,
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

			var database = mongoClient.GetDatabase("testDB");
			var collection = database.GetCollection<Author>("library");
			//collection.InsertOne(author1);
			//collection.InsertOne(author2);
			//collection.InsertOne(author3);

			var result = collection.Find(x => !x.Deleted && x.Books.Any(b => !b.Deleted))
				.Project(x => new Author
				{
					Id = x.Id,
					Name = x.Name,
					Books = x.Books.Where(b => !b.Deleted).ToList(),
					Deleted = x.Deleted,
				}) .ToList();
		}
	}
}
