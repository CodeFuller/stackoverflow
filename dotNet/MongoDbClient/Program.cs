using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace MongoDbClient
{
	public class SomeDocument
	{
		public ObjectId Id { get; set; }

		[BsonElement("time")]
		public DateTime Time { get; set; }
	}

	public class AggregatedDateTime
	{
		[BsonElement("year")]
		public int Year { get; set; }

		[BsonElement("month")]
		public int Month { get; set; }

		[BsonElement("day")]
		public int Day { get; set; }

		[BsonElement("hour")]
		public int Hour { get; set; }

		[BsonElement("minute")]
		public int Minute { get; set; }
	}

	public class DateTimeStats
	{
		public AggregatedDateTime Id { get; set; }

		[BsonElement("count")]
		public int Count { get; set; }
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
			var collection = database.GetCollection<SomeDocument>("foo");

			var project = new BsonDocument
			{
				{
					"$project",
					new BsonDocument
					{
						{
							"date",
							new BsonDocument
							{
								{ "year", new BsonDocument { {"$year", "$time"} } },
								{ "month", new BsonDocument { {"$month", "$time" } } },
								{ "day", new BsonDocument { { "$dayOfMonth", "$time" } } },
								{ "hour", new BsonDocument { { "$hour", "$time" } } },
								{ "minute",
									new BsonDocument
									{
										{
											"$subtract",
											new BsonArray
											{
												new BsonDocument { { "$minute", "$time"} },
												new BsonDocument
												{
													{
														"$mod", new BsonArray
														{
															new BsonDocument { { "$minute", "$time"} },
															10
														}
													}
												},
											}
										}
									}
								},
							}
						}
					}
				}
			};

			var group = new BsonDocument
			{
				{ "$group",
					new BsonDocument
					{
						{"_id", "$date"},
						{"count", new BsonDocument("$sum", 1)}
					}
				}
			};

			var pipeline = new[] { project, group };
			var results = collection.Aggregate<DateTimeStats>(pipeline).ToList();

			var result2 = collection.AsQueryable().Select(x => new
			{
				Date = new
				{
					Year = x.Time.Year,
					Month = x.Time.Month,
					Day = x.Time.Day,
					Hour = x.Time.Hour,
					Minute = x.Time.Minute - x.Time.Minute % 10,
				}
			}).GroupBy(x => x.Date).Select(g => new
			{
				Date = g.Key,
				Count = g.Count(),
			}).ToList();
		}
	}
}
