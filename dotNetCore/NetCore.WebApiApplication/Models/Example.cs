using System.Collections.Generic;

namespace NetCore.WebApiApplication.Models
{
	public class Example
	{
		public Example(string name)
		{
			Name = name;
		}

		public string Name { get; }

		public IEnumerable<Example> Demos
		{
			get { yield return this; }
		}
	}
}
