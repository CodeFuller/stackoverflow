using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		// GET api/values
		[HttpGet]
		public IEnumerable<string> Get()
		{
			var user = new
			{
				Id = "123",
				UserName = "Some User",
			};

			var _appSettings = new
			{
				Token = new
				{
					Key = "Some long key goes here",
					Issuer = "Some Issuer",
					DownloadTokenExpireMin = 1000,
				}
			};

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
				new Claim(JwtRegisteredClaimNames.NameId, user.Id),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

			string tokenSerialized;

			{
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Token.Key));
				var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

				var token = new JwtSecurityToken(_appSettings.Token.Issuer,
					_appSettings.Token.Issuer,
					claims,
					expires: DateTime.Now.AddMinutes(_appSettings.Token.DownloadTokenExpireMin),
					signingCredentials: creds);

				var tokenHandler = new JwtSecurityTokenHandler();
				tokenSerialized = tokenHandler.WriteToken(token);
			}

			{
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Token.Key));

				TokenValidationParameters validationParameters =
					new TokenValidationParameters
					{
						ValidIssuer = _appSettings.Token.Issuer,
						ValidAudiences = new[] { _appSettings.Token.Issuer },
						IssuerSigningKeys = new[] { key }
					};

				// Now validate the token. If the token is not valid for any reason, an exception will be thrown by the method
				SecurityToken validatedToken;
				JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
				var userFromToken = handler.ValidateToken(tokenSerialized, validationParameters, out validatedToken);
			}

			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
