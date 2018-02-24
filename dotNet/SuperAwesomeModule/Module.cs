using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SuperAwesomeModule
{
    public class Module : BaseHttpModule
    {
        private static Stopwatch _internalStopwatch;
        private const string XResponseTimeHeaderName = "X-ResponseTime";
        private static readonly List<LoadDTO> LoadDtos = new List<LoadDTO>();
        
        public Module()
        {
            _internalStopwatch = Stopwatch.StartNew();
        }
        
        public override void PreRequestHandlerExecute(HttpContextBase context)
        {
            // Add a filter to capture response stream
            context.Response.Filter = new ResponseSniffer(context.Response.Filter);
        }

        public override void ApplicationEndRequest(HttpContextBase context)
        {
            try
            {
                if (context == null) return;
                
                if (!context.Items.Contains(XResponseTimeHeaderName)) return;
                var stopwatch = (Stopwatch)context.Items[XResponseTimeHeaderName];
                stopwatch.Stop();
                var responseTime = Math.Ceiling(stopwatch.Elapsed.TotalMilliseconds);
                context.Response.AddHeader(XResponseTimeHeaderName, $"{responseTime} ms");
                var length = 0m;
                if (context.Response.Filter is ResponseSniffer filter)
                {
                    var streamReader = new StreamReader(new MemoryStream(filter.RecordStream.GetBuffer()));
                    length = streamReader.BaseStream.Length;
                }

                _internalStopwatch.Stop();
                var internalRequestTimeAsString = Math.Ceiling(_internalStopwatch.Elapsed.TotalMilliseconds);
                LoadDtos.Add(new LoadDTO
                {
                    RequestTime = responseTime,
                    InternalTime = Math.Ceiling(_internalStopwatch.Elapsed.TotalMilliseconds),
                    ResponseSize = length
                });

                var filePathExtension = Path.GetExtension(context.Request.FilePath);
                if (!string.IsNullOrEmpty(filePathExtension) || context.Request.FilePath.StartsWith("/api")) return; //likely static file or an api route

                var builder = new StringBuilder()
                    .Append($"<hr/>Request took {responseTime} ms<br/>")
                    .Append($"Response Size: {length} bytes<br/>")
                    .Append($"Module request time: {internalRequestTimeAsString} ms")
                    .Append("<hr/>Averages:<br/>")
                    .Append($"Request {Math.Round(LoadDtos.Average(x => x.RequestTime), 2)} ms<br/>")
                    .Append($"Response Size: {Math.Round(LoadDtos.Average(x => x.ResponseSize), 2)} bytes<br/>")
                    .Append($"Module request time: {Math.Round(LoadDtos.Average(x => x.InternalTime), 2)} ms");

                context.Response.Write(builder.ToString());
            }
            catch (Exception exception)
            {
                var a = 1;
            }
        }

        public override void ApplicationBeginRequest(HttpContextBase context)
        {
            try
            {
                if (context == null) return;
                var stopwatch = Stopwatch.StartNew();
                context.Items.Add(XResponseTimeHeaderName, stopwatch);
            }
            catch (Exception exception)
            {
                var a = 1;
            }
        }
    }
}
