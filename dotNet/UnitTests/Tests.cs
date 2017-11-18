using System.Globalization;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class Tests
	{
		[TestCase("ABC", "abc")]
		public void TestMethod(string inputData, string expectedResult)
		{
			var result = inputData.ToLower(CultureInfo.InvariantCulture);

			Assert.AreEqual(expectedResult, result);
		}
	}
}
