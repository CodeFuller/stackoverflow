using System.ComponentModel.DataAnnotations;

namespace NetCore.WebApiApplication
{
	public class MyModelClass
	{
		//[ValidateIntegerValue(ErrorMessage = "{0} must be a Integer Value")]
		[Required(ErrorMessage = "{0} is required")]
		public int Level { get; set; }
	}
}
