using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApplication.Objects
{
	public class Album
	{
		public Guid Id { get; set; }

		public string Title { get; set; }

		[ForeignKey("Artist_Id")]
		public Artist Artist { get; set; }
	}
}
