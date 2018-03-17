using System;
using MongoDB.Bson.Serialization;

namespace MongoDbClient
{
	public class StringGuidGenerator : IIdGenerator
	{
		public object GenerateId(object container, object document)
		{
			return Guid.NewGuid().ToString();
		}

		public bool IsEmpty(object id)
		{
			return id == null;
		}
	}
}
