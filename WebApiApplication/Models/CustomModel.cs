using System.ComponentModel;

namespace WebApiApplication.Models
{
	[TypeConverter(typeof(CustomModelConverter))]
	public class CustomModel
	{
	}
}
