namespace MongoDbClient
{
	public class Parameter
	{
		//[BsonIgnoreIfDefault]
		//[BsonId(IdGenerator = typeof(StringGuidGenerator))]
		//public string Id { get; set; }

		//[BsonId]
		public string Name { get; set; }

		public string Unit { get; set; }

		public ParameterType ParameterType { get; set; }
	}
}
