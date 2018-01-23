using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace WebApiApplication
{
	public class UriOrBodyParameterBinding : HttpParameterBinding
	{
		private readonly HttpParameterDescriptor paramDescriptor;

		public UriOrBodyParameterBinding(HttpParameterDescriptor descriptor) : base(descriptor)
		{
			paramDescriptor = descriptor;
		}

		public override async Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext,
			CancellationToken cancellationToken)
		{
			HttpParameterBinding binding = actionContext.Request.Content.Headers.ContentLength > 0
				? new FromBodyAttribute().GetBinding(paramDescriptor)
				: new FromUriAttribute().GetBinding(paramDescriptor);

			await binding.ExecuteBindingAsync(metadataProvider, actionContext, cancellationToken);
		}
	}
}
