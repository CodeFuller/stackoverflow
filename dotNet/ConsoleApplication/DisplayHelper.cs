using System;
using Microsoft.Office.Interop.Word;

namespace ConsoleApplication
{
	public static class DisplayHelper
	{
		public static string DisplayRange(MarshalByRefObject obj)
		{
			var range = obj as Range;
			return range?.Text ?? obj?.ToString() ?? "The value is null";
		}
	}
}
