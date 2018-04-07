using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApiApplication
{
	public class CustomMiddleware
	{
		private readonly RequestDelegate _next;

		public CustomMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public Task InvokeAsync(HttpContext context)
		{
			throw new InvalidOperationException();
			return this._next(context);
		}
	}
}
