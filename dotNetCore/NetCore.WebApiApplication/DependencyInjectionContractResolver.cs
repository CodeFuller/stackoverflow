using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace NetCore.WebApiApplication
{
	public class DependencyInjectionContractResolver : DefaultContractResolver
	{
		private readonly IServiceProvider serviceProvider;
		private readonly List<Type> registeredTypes;

		public DependencyInjectionContractResolver(IServiceProvider serviceProvider, IEnumerable<Type> registeredTypes)
		{
			this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
			this.registeredTypes = registeredTypes?.ToList() ?? throw new ArgumentNullException(nameof(registeredTypes));
		}

		protected override JsonContract CreateContract(Type objectType)
		{
			JsonContract contract = base.CreateContract(objectType);

			if (registeredTypes.Contains(objectType))
			{
				contract.DefaultCreator = () => serviceProvider.GetService(objectType);
			}

			return contract;
		}
	}
}
