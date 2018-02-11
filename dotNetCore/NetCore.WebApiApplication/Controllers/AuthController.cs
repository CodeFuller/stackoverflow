using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetCore.WebApiApplication.Models;

namespace NetCore.WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class AuthController : Controller
	{
		private readonly UserModel model;
		private readonly IConfiguration _config;

		public AuthController(IConfiguration config)
		{
			model = new UserModel
			{
				FirstName = "Code",
				LastName = "Fuller",
				Email = "CodeFuller@gmail.com",
			};

			_config = config;
		}

		[HttpGet]
		public string Get()
		{
			var claims = new[] {
				new Claim (JwtRegisteredClaimNames.Sub, model.Email),
				new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString()),
			};

			//_config
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expiration = DateTime.UtcNow.AddDays(7);
			var token = new JwtSecurityToken(_config["Tokens:Issuer"],
				_config["Tokens:Issuer"],
				claims,
				expires: expiration,
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
