using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApplication.Models
{
	public sealed class TooltipAttribute : Attribute
	{
		public TooltipAttribute(string tooltip)
		{
		}
	}

	public class UserModel
	{
		[Tooltip("Please enter your name.")]
		public string Name { get; set; }
	}
}
