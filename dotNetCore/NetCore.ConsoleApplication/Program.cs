using System.Threading;

namespace NetCore.ConsoleApplication
{
	public class SomeSettings
	{
		public string SomeValue { get; set; }
	}

	class Program
	{
		static void Main(string[] args)
		{
			var repository = new Repository();
			repository.M(new Model(), CancellationToken.None).Wait();
		}
	}
}
