using System;
using System.Collections.Generic;

namespace WebApiApplication.Models
{
	public class Artist
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public ICollection<Album> Songs { get; set; }
	}
}
