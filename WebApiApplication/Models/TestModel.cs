using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApplication.Models
{
	public class TestModel
	{
		[Required]
		public int? Id { get; set; }

		[EnumDataType(typeof(MyEnum), ErrorMessage = "Custom Error Message")]
		public MyEnum? MyEnumProp { get; set; }
	}
}
