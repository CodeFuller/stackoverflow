using System;
using System.Collections.Generic;

namespace NetCore.ConsoleApplication
{
	public static class ObjectExtensions
	{
		public static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs(this Object settings, string settingsRoot)
		{
			foreach (var property in settings.GetType().GetProperties())
			{
				yield return new KeyValuePair<string, string>($"{settingsRoot}:{property.Name}", property.GetValue(settings).ToString());
			}
		}
	}
}
