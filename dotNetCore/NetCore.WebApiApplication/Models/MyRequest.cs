using System;
using System.ComponentModel.DataAnnotations;

namespace NetCore.WebApiApplication.Models
{
	public class MyRequest
	{
		[Required]
		public Guid? Id { get; set; }

		[Required]
		public DateTime? EndDateTimeUtc { get; set; }

		[Required]
		public DateTime? StartDateTimeUtc { get; set; }
	}
}
