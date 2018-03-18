using System.Text.RegularExpressions;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = @"http://tempuri.org/IService1/GetData\";
			Regex regex = new Regex("/(\\w*)\\\\$");
			var match = regex.Match(input);
			if (match.Success)
			{
				//  data = "GetData"
				var data = match.Groups[1].Value;
			}
		}
	}
}
