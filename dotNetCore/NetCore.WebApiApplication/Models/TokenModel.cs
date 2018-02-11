using System;

namespace NetCore.WebApiApplication.Models
{
	public class TokenModel
	{
		public string Token { get; set; }

		public DateTime Expiration { get; set; }

		public string UserFirstName { get; set; }

		public object UserLastName { get; set; }
	}
}
