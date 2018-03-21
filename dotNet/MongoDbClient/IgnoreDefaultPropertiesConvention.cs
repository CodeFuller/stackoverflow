using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace MongoDbClient
{
	public class IgnoreDefaultPropertiesConvention : IMemberMapConvention
	{
		public string Name => "Ignore default properties for all classes";

		public void Apply(BsonMemberMap memberMap)
		{
			memberMap.SetIgnoreIfDefault(true);
		}
	}
}
