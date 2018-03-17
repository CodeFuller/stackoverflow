namespace MongoDbClient
{
	public class ParameterType
	{
		public string Type { get; set; }

		public static ParameterType TestResult => new ParameterType { Type = "TestParameterType" };
	}
}
