namespace ConsoleApplication
{
class SomeValue
{
	public string Foo { get; set; }

	public int Code { get; set; }
}

	class Program
	{
		
		static SomeValue ExampleFunction(string input)
		{
			return new SomeValue
			{
				Foo = input,
				Code = 1,
			};
		}

		static void Main(string[] args)
		{
			string foo0 = null;
			string foo1 = null;
			string foo2 = null;
			int Type = 1;

			SomeValue val = null;
			switch (Type)
			{
				case 0: val = ExampleFunction(foo0); break;
				case 1: val = ExampleFunction(foo1); break;
				case 2: val = ExampleFunction(foo2); break;
			}
		}
	}
}
