using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using WebApiApplication.Controllers;

namespace WebApiApplication
{
	public class AuthRequestBinding : HttpParameterBinding
	{
		private readonly HttpParameterDescriptor paramDescriptor;

		public AuthRequestBinding(HttpParameterDescriptor descriptor) : base(descriptor)
		{
			paramDescriptor = descriptor;
		}

		public override async Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext,
			CancellationToken cancellationToken)
		{
			var defaultBinder = new FromBodyAttribute().GetBinding(paramDescriptor);
			await defaultBinder.ExecuteBindingAsync(metadataProvider, actionContext, cancellationToken);

			AuthenticatedRequest authRequest = actionContext.ActionArguments[paramDescriptor.ParameterName] as AuthenticatedRequest;
			if (authRequest != null)
			{
				IEnumerable<string> headerValues;
				if (actionContext.Request.Headers.TryGetValues("Your-Custom-Header-Here", out headerValues))
				{
					authRequest.AuthToken = headerValues.FirstOrDefault();
				}
			}
		}
	}
}
