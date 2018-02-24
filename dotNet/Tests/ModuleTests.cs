using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SuperAwesomeModule;

namespace Tests
{
    [TestClass]
    public class ModuleTests
    {
        [TestMethod]
        public void PreRequestHandlerExecuteShouldSetResponseSnifferAsFilter()
        {
            //  Arrange

            Stream filter = null;
            Mock<HttpResponseBase> httpResponseMock = new Mock<HttpResponseBase>();
            httpResponseMock.SetupSet(response => response.Filter = It.IsAny<Stream>()).Callback<Stream>(value => filter = value);

            Mock<HttpContextBase> httpContextStub = new Mock<HttpContextBase>();
            httpContextStub.SetupGet(x => x.Response).Returns(httpResponseMock.Object);

            var target = new Module();

            //  Act

            target.PreRequestHandlerExecute(httpContextStub.Object);

            //  Assert

            Assert.IsNotNull(filter);
            Assert.IsInstanceOfType(filter, typeof(ResponseSniffer));
        }

        [TestMethod]
        public void ApplicationBeginRequestShouldStoreStopwatchInContextItems()
        {
            //  Arrange

            var items = new Dictionary<string, object>();

            Mock<HttpContextBase> httpContextStub = new Mock<HttpContextBase>();
            httpContextStub.SetupGet(x => x.Items).Returns(items);

            var target = new Module();

            //  Act

            target.ApplicationBeginRequest(httpContextStub.Object);

            //  Assert

            Assert.IsTrue(items.ContainsKey("X-ResponseTime"));
            Assert.IsInstanceOfType(items["X-ResponseTime"], typeof(Stopwatch));
        }

        [TestMethod]
        public void ApplicationEndRequestShouldAddRequestInfoToResponse()
        {
            //  Arrange

            Mock<HttpRequestBase> httpRequestMock = new Mock<HttpRequestBase>();
            httpRequestMock.SetupGet(x => x.FilePath).Returns("/test");

            string writtenData = null;
            Mock<HttpResponseBase> httpResponseMock = new Mock<HttpResponseBase>();
            httpResponseMock.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s => writtenData = s);

            Mock<HttpContextBase> httpContextStub = new Mock<HttpContextBase>();
            httpContextStub.SetupGet(x => x.Request).Returns(httpRequestMock.Object);
            httpContextStub.SetupGet(x => x.Response).Returns(httpResponseMock.Object);
            httpContextStub.SetupGet(x => x.Items).Returns(new Dictionary<string, object> { ["X-ResponseTime"] = new Stopwatch() });

            var target = new Module();

            //  Act

            target.ApplicationEndRequest(httpContextStub.Object);

            //  Assert

            Assert.IsTrue(Regex.IsMatch(writtenData, @"Response Size: \d+ bytes<br/>"));
            Assert.IsTrue(Regex.IsMatch(writtenData, @"Module request time: \d+ ms"));
        }
    }
}
