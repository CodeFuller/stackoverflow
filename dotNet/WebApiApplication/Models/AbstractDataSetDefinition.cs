using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiApplication.Models
{
	[Serializable]
	public abstract class AbstractDataSetDefinition// : IMongoDocumentModel
	{
		[BsonId]
		public string _id { get; set; }
		public int CompanyID { get; set; }
		//The left most data feed, which must be the new data that is being processed. Other data sources are joined on this data feed.
		public string BaseFeed { get; set; }
		//The source system of the base feed.
		public string BaseSourceSystem { get; set; }
		//An array of DataFeedJoin objects can pull in data from other data feeds.
		public List<DataFeedJoin> Joins { get; set; }

		//public abstract AbstractDataSet GenerateDataSet(RawData rd, Dictionary<int, RawData> rawDataComponents);
	}
}
