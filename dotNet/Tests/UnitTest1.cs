using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperAwesomeModule;

namespace Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mockHttpContext = new MoqHttpContext();

            var httpContext = mockHttpContext.HttpContext();
            var html = @"<html>
                            <head></head>
                            <body>
                                <h1>Hello World</h1>
                            </body>
                        </html>";

            var module = new Module();
            module.PreRequestHandlerExecute(mockHttpContext.HttpContext());

            httpContext.ResponseWrite(html);
            httpContext.StreamWrite(httpContext.Response.Filter, html);

            module.ApplicationBeginRequest(mockHttpContext.HttpContext());
            module.ApplicationEndRequest(mockHttpContext.HttpContext());

            var responseRead = httpContext.ResponseRead();
            var b = 1; //todo assert that body has changed e.g. responseRead != html (includes modifications from module)
        }
    }
}
