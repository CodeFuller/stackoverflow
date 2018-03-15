using System;

namespace NetCore.WebApiApplication
{
	public class Measuring : IDisposable
	{
		private readonly DateTimeOffset startDateTime = DateTimeOffset.Now;

		public void Dispose()
		{
			var requestTime = DateTimeOffset.Now - startDateTime;
			Console.WriteLine($"Request time: {requestTime}");
		}
	}
}
