using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace WebApiApplication.Models
{
	public class ReportingSetDefinition
	{
		public int Id { get; set; }

		public int NumericProperty { get; set; }

		public string StringProperty { get; set; }

		public BsonValue TestValue { get; set; }
	}
}
