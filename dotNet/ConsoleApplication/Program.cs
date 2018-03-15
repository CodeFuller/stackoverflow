using System;
using System.Threading;

namespace ConsoleApplication
{
	class Program
	{
		private static BaseTCPSocketListener bsl;

		static void DoTest()
		{
			if (bsl == null)
			{
				bsl = new BaseTCPSocketListener();
				bsl.OnDataReceived += delegate (object o, string s)
				{
					Console.WriteLine($"Base: {DateTime.Now} - {s}");
				};

				bsl.OnError += delegate (object o, Exception x)
				{
					Console.WriteLine($"Base TCP receiver error: {DateTime.Now} - {x.Message}");
				};

			}

			try
			{
				bsl.Init("127.0.0.1|80|10");
				Console.WriteLine("BEGIN RECEIVING BSL data --------------------------");
			}
			catch (Exception exception)
			{
				Console.WriteLine($"ERROR CONNECTING TO BSL ------------{exception.Message}");
			}
		}

		static void Main(string[] args)
		{
			DoTest();
			Thread.Sleep(5000);
			DoTest();
			Thread.Sleep(5000);
			DoTest();
		}
	}
}
