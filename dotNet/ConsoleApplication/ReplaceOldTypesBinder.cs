using System;
using Newtonsoft.Json.Serialization;

namespace ConsoleApplication
{
	public class ReplaceOldTypesBinder : ISerializationBinder
	{
		public Type BindToType(string assemblyName, string typeName)
		{
			//	Put your logic for replacing type here
			if (assemblyName == "ConsoleApplication" && typeName == "ConsoleApplication.OldType")
			{
				return typeof(NewType);
			}

			return Type.GetType(typeName);
		}

		public void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			throw new NotImplementedException();
		}
	}
}
