using System.Threading.Tasks;
using MongoDB.Driver;
using NSubstitute;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class Tests
	{
		[Test]
		public void TestMethod()
		{
			var cursorMock = Substitute.For<IAsyncCursor<string>>();
			cursorMock.MoveNextAsync().Returns(Task.FromResult(true), Task.FromResult(false));
			cursorMock.Current.Returns(new[] { "asd" });

			var ff = Substitute.For<IFindFluent<string, string>>();
			ff.ToCursorAsync().Returns(Task.FromResult(cursorMock));
			ff.Limit(1).Returns(ff);

			var result = ff.FirstOrDefaultAsync().Result;
			Assert.AreEqual("asd", result);
		}
	}
}
