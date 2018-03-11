using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiApplication.Models
{
	[Serializable]
	public class SimpleDataFieldDefinition : AbstractDataFieldDefinition
	{
		[BsonIgnoreIfNull]
		public int? JoinID { get; set; }
		[BsonIgnoreIfNull]
		public string Field { get; set; }
		[BsonIgnoreIfNull]
		public BsonValue DefaultValue { get; set; }
		[BsonIgnoreIfNull]
		public bool? IsDate { get; set; }

		public SimpleDataFieldDefinition() { }

		public SimpleDataFieldDefinition(BsonValue defValue, bool isDate)
		{
			DefaultValue = defValue;
			IsDate = isDate;
		}
	}
}
