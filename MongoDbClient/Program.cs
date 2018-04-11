using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace MongoDbClient
{
	class Program
	{
		static void Main(string[] args)
		{
			var mongoClient = new MongoClient(new MongoClientSettings
			{
				Server = new MongoServerAddress("localhost", 27017)
			});

			var database = mongoClient.GetDatabase("testDB");
			IMongoCollection<SomeDocument> collection = database.GetCollection<SomeDocument>("testCollection");
			collection.Indexes.CreateOne(new IndexKeysDefinitionBuilder<SomeDocument>().Geo2DSphere(x => x.Location));

			//	Deleting previous data
			collection.DeleteMany(FilterDefinition<SomeDocument>.Empty);

			collection.InsertOne(new SomeDocument
			{
				Title = "Place #1",
				Location = GeoJson.Point(new GeoJson2DGeographicCoordinates(145.89, -35.83)),
			});

			collection.InsertOne(new SomeDocument
			{
				Title = "Place #2",
				Location = GeoJson.Point(new GeoJson2DGeographicCoordinates(154.98, -53.38)),
			});

			var point = GeoJson.Point(new GeoJson2DGeographicCoordinates(145.889, -35.831));
			int maxDistance = 300;

			IAsyncCursor<SomeDocument> cursor = collection.FindSync(new FilterDefinitionBuilder<SomeDocument>().Near(x => x.Location, point, maxDistance: maxDistance));
			//	Check whether at least one is near the point
			var hasNeighbors = cursor.Any();
		}
	}
}
