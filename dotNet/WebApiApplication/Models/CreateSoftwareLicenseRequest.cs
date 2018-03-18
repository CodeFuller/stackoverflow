namespace WebApiApplication.Models
{
	public class CreateSoftwareLicenseRequest
	{
		/// <summary>
		/// Some description for OEM Code.
		/// </summary>
		public int OEMCode { get; set; }

		/// <summary>
		/// Some description for Part Number.
		/// </summary>
		public string PartNumber { get; set; }
	}
}
