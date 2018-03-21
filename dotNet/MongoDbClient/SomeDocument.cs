using MongoDB.Bson;

namespace MongoDbClient
{
	public class SomeDocument
	{
		public ObjectId Id { get; set; }

		public string StringData { get; set; }

		public int NumericData { get; set; }
	}
}
