namespace NetCore.WebApiApplication.Models
{
	public class TestInput
	{
		public string IWork { get; set; }

		public ITestInterface IDontWork { get; set; }
	}
}
