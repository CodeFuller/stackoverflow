using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
