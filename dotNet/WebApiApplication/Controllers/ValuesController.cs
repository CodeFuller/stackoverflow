using System.Web.Http;
using Microsoft.Web.Http;
using WebApiApplication.Models;

namespace WebApiApplication.Controllers
{
	/// <summary>
	/// Controller for the License api.
	/// </summary>
	/// 
	[ApiVersion("1.0")]
	[RoutePrefix("api/v{version:apiVersion}/license")]
	public class LicenseController : ApiController
	{
		/// <summary>
		/// Creates a new Software License.
		/// </summary>
		/// <param name="value">The parameters for the license.</param>
		/// <returns>The newly created Activation and Emergency Ids.</returns>
		[Route("software")]
		[HttpPost]
		public LicenseCreateResponse CreateSoftwareLicense([FromBody] CreateSoftwareLicenseRequest value)
		{
			// License creation code
			return new LicenseCreateResponse();
		}
	}
}
