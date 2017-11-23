using System;

namespace ConsoleApplication
{
public static class FloatExtensions
{
	public static string Format(this float f, int n)
	{
		//return f.ToString($"0.{new String('0', n)}");
		return f.ToString("0." + new String('0', n));
	}
}


	class Program
	{
		static void Main(string[] args)
		{
			float rmt = 100;
			int n = 2;

			var str = rmt.Format(n);
		}
	}
}
