using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SuperAwesomeModule;

namespace Tests
{
    [TestClass]
    public class ResponseSnifferTests
    {
        [TestMethod]
        public void Write_WritesDataToRecordStream()
        {
            //  Arrange

            var inData = new byte[] { 0x01 };

            var target = new ResponseSniffer(Mock.Of<Stream>());

            //  Act

            target.Write(inData, 0, inData.Length);

            //  Assert

            target.RecordStream.Position = 0;
            var outData = new byte[inData.Length];
            int outSize = target.RecordStream.Read(outData, 0, outData.Length);
            Assert.AreEqual(inData.Length, outSize);
            CollectionAssert.AreEqual(inData, outData);
        }
    }
}
