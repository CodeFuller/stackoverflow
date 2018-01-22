using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace MongoDbClient
{
	public class TestDocument : Document
	{
		public int NumericData { get; set; }

		public string StringData { get; set; }
	}

	public class FixingReferralsSerializer : EnumerableSerializerBase<List<ObjectId?>>
	{
		public override List<ObjectId?> Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			if (context.Reader.CurrentBsonType == BsonType.String)
			{
				context.Reader.ReadString();
				return null;
			}

			return base.Deserialize(context, args);
		}

		protected override void AddItem(object accumulator, object item)
		{
			((List<ObjectId?>)accumulator).Add((ObjectId?)item);
		}

		protected override object CreateAccumulator()
		{
			return new List<ObjectId?>();
		}

		protected override IEnumerable EnumerateItemsInSerializationOrder(List<ObjectId?> value)
		{
			return value;
		}

		protected override List<ObjectId?> FinalizeResult(object accumulator)
		{
			return (List<ObjectId?>)accumulator;
		}
	}

	public class FixingReferralsArraySerializer : ArraySerializer<ObjectId?>
	{
		public override ObjectId?[] Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			if (context.Reader.CurrentBsonType == BsonType.String)
			{
				context.Reader.ReadString();
				return null;
			}

			return base.Deserialize(context, args);
		}
	}

	[BsonIgnoreExtraElements]
	public class User : Document
	{
		[BsonDefaultValue(null)]
		[BsonSerializer(typeof(FixingReferralsSerializer))]
		public List<ObjectId?> referrals { get; set; }
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
			var collection = database.GetCollection<User>("TestUsers");

			var data = collection.AsQueryable<User>().ToList();
		}
	}
}
