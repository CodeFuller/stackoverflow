using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Hosting;

namespace WebApiApplication
{
	public class GetBindingHostedService : IHostedService
	{
		public static IServerAddressesFeature ServerAddresses { get; set; }

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var address = ServerAddresses.Addresses.Single();
			var match = Regex.Match(address, @"^.+:(\d+)$");
			if (match.Success)
			{
				int port = Int32.Parse(match.Groups[1].Value);
				Console.WriteLine($"Bound port is {port}");
			}

			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
