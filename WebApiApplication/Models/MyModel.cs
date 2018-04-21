using System.ComponentModel.DataAnnotations;

namespace WebApiApplication.Models
{
	public class MyModel
	{
		[EnumDataType(typeof(MyEnum))]
		public MyEnum? MyEnum { get; set; }
	}
}
