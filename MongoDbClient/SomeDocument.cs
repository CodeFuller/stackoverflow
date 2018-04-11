using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;

namespace MongoDbClient
{
	public class SomeDocument
	{
		public ObjectId Id { get; set; }

		public string Title { get; set; }

		public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }
	}
}
