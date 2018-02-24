using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Web;
using Moq;
using SuperAwesomeModule;

namespace Tests
{
    //adapted from https://o2platform.wordpress.com/2011/04/05/mocking-httpcontext-httprequest-and-httpresponse-for-unittests-using-moq/
    public class MoqHttpContext
    {
        public Mock<HttpContextBase> MockContext { get; set; }
        public Mock<HttpRequestBase> MockRequest { get; set; }
        public Mock<HttpResponseBase> MockResponse { get; set; }
        public Mock<HttpSessionStateBase> MockSession { get; set; }
        public Mock<HttpServerUtilityBase> MockServer { get; set; }
        public Mock<IPrincipal> MockUser { get; set; }
        public Mock<IIdentity> MockIdentity { get; set; }

        public HttpContextBase HttpContextBase { get; set; }
        public HttpRequestBase HttpRequestBase { get; set; }
        public HttpResponseBase HttpResponseBase { get; set; }

        private readonly MemoryStream _outputStream = new MemoryStream();

        public MoqHttpContext()
        {
            CreateBaseMocks();
            SetupNormalRequestValues();
        }

        public MoqHttpContext CreateBaseMocks()
        {
            MockContext = new Mock<HttpContextBase>();
            MockRequest = new Mock<HttpRequestBase>();
            MockResponse = new Mock<HttpResponseBase>();
            MockSession = new Mock<HttpSessionStateBase>();
            MockServer = new Mock<HttpServerUtilityBase>();

            MockResponse.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(s =>
            {
                var data = Encoding.ASCII.GetBytes(s);
                _outputStream.Write(data, 0, data.Length);
                _outputStream.Flush();
                _outputStream.Position = 0;
            });

            MockContext.Setup(ctx => ctx.Request).Returns(MockRequest.Object);
            MockContext.Setup(ctx => ctx.Response).Returns(MockResponse.Object);
            MockContext.Setup(ctx => ctx.Session).Returns(MockSession.Object);
            MockContext.Setup(ctx => ctx.Server).Returns(MockServer.Object);
            MockContext.Setup(context => context.Items).Returns(new Dictionary<object, object>());


            HttpContextBase = MockContext.Object;
            HttpRequestBase = MockRequest.Object;
            HttpResponseBase = MockResponse.Object;

            return this;
        }

        public MoqHttpContext SetupNormalRequestValues()
        {
            //Context.User
            var mockUser = new Mock<IPrincipal>();
            var mockIdentity = new Mock<IIdentity>();
            MockContext.Setup(context => context.User).Returns(mockUser.Object);
            //MockContext.SetupGet(context => context.Response.OutputStream).Returns(new MemoryStream());
            //MockContext.SetupGet(context => context.Response.Filter).Returns(new MemoryStream());

            mockUser.Setup(context => context.Identity).Returns(mockIdentity.Object);

            //Request
            MockRequest.Setup(request => request.InputStream).Returns(new MemoryStream());
            MockRequest.Setup(request => request.FilePath).Returns("/");

            //Response

            //MockResponse.Setup(response => response.OutputStream).Returns(GetMockStream(outputStream).Object);
            //MockResponse.Setup(response => response.Filter).Returns(GetMockStream(filter).Object);

            MockResponse.Setup(response => response.OutputStream).Returns(() => _outputStream);
            //MockResponse.SetupSet(response => response.OutputStream = It.IsAny<Stream>()).Returns(() => outputStream);

            Stream filter = new MemoryStream();
            MockResponse.SetupSet(response => response.Filter = It.IsAny<Stream>()).Callback<Stream>(value => filter = value);
            MockResponse.SetupGet(response => response.Filter).Returns(() => filter);

            //var outputStream = new MockStream();
            //var filter = new MockStream();
            //MockResponse.Setup(response => response.OutputStream).Returns(outputStream);
            //MockResponse.Setup(response => response.Filter).Returns(filter);
            return this;
        }

        private Mock<Stream> GetMockStream(Stream stream)
        {
            var mockStream = new Mock<Stream>();
            mockStream.Setup(s => s.CanWrite).Returns(true);
            mockStream.Setup(s => s.CanRead).Returns(true);
            mockStream.Setup(s => s.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                .Callback((byte[] buffer, int offset, int count) =>
                {
                    stream.Write(buffer, offset, count);
                });
            mockStream.Setup(s => s.Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                .Callback((byte[] buffer, int offset, int count) =>
                {
                    stream.Read(buffer, offset, count);
                });
            //mockStream.SetupSet()
            return mockStream;
        }
    }
}

