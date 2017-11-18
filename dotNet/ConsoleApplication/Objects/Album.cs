using System;

namespace ConsoleApplication.Objects
{
	public class Album
	{
		public Guid Id { get; set; }

		public string Title { get; set; }

		public Artist Artist { get; set; }
	}
}
