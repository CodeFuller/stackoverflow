using System.Text.RegularExpressions;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class Tests
	{
		[TestCase("abbb", true)]
		[TestCase("cddd", true)]
		[TestCase("abc", false)]
		public void TestMethod(string inputData, bool expectedResult)
		{
			Regex regex = new Regex(@"\b[A-Za-z]([A-Za-z])\1+\b");
			var match = regex.Match(inputData);

			Assert.AreEqual(expectedResult, match.Success);
		}
	}
}
