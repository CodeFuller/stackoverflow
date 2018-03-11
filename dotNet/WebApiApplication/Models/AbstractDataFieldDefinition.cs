using System;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiApplication.Models
{
	[Serializable]
	//[BsonKnownTypes(typeof(SimpleDataFieldDefinition), typeof(ComplexNumericDataFieldDefinition), typeof(ComplexStringDataFieldDefinition), typeof(ComplexDateDataFieldDefinition), typeof(ConditionalDataFieldDefinition))]
	[BsonKnownTypes(typeof(SimpleDataFieldDefinition))]
	public abstract class AbstractDataFieldDefinition
	{
		//public abstract BsonValue GetFieldValue(Dictionary<int, RawData> RawDataComponents);

		//public abstract Dictionary<string, string> GetDetails();
	}
}
