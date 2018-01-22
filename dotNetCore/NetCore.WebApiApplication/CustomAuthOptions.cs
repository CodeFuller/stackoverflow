using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NetCore.WebApiApplication
{
	public class CustomAuthOptions : AuthenticationSchemeOptions
	{
		public ClaimsIdentity Identity { get; set; }

		public CustomAuthOptions()
		{

		}
	}

	public static class CustomAuthExtensions
	{
		public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, Action<CustomAuthOptions> configureOptions)
		{
			return builder.AddScheme<CustomAuthOptions, CustomAuthHandler>("Custom Scheme", "Custom Auth", configureOptions);
		}
	}

	internal class CustomAuthHandler : AuthenticationHandler<CustomAuthOptions>
	{
		public CustomAuthHandler(IOptionsMonitor<CustomAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
		{

		}

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			string token = Request.Headers["Authorization"];
			if (string.IsNullOrEmpty(token))
				return AuthenticateResult.Fail("Failing string");

			// Using external service to validate token and get user id
			int Id = GetUserId(token);

			//var principal = new ClaimsPrincipal(new ClaimsIdentity(
			return AuthenticateResult.Success(
				new AuthenticationTicket(
					new ClaimsPrincipal(
						new ClaimsIdentity(
							new List<Claim>() { new Claim(ClaimTypes.Sid, Id.ToString()) }, Scheme.Name)),
					Scheme.Name));
		}

		private int GetUserId(string token)
		{
			return 12345;
		}
	}
}
