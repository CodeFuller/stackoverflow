namespace NetCore.ConsoleApplication
{
	public class PropertyValue
	{
		public string Name { get; set; }

		public object Value { get; set; }

		public bool DestructureObjects { get; set; }

		public PropertyValue(string name, object value, bool destructureObjects)
		{
			Name = name;
			Value = value;
			DestructureObjects = destructureObjects;
		}
	}
}
