using System;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiApplication
{
	public class DbObjectFactory : IDbObjectFactory
	{
		private readonly IServiceProvider serviceProvider;

		public DbObjectFactory(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
		}

		public TDbObject Create<TDbObject>() where TDbObject : DbObject<TDbObject>
		{
			return serviceProvider.GetRequiredService<TDbObject>();
		}
	}
}
