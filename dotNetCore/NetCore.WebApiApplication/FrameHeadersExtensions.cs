using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Primitives;

namespace NetCore.WebApiApplication
{
	public static class FrameHeadersExtensions
	{
		public static void MakeCaseInsensitive(this FrameHeaders target)
		{
			var fieldInfo = GetDictionaryField(target.GetType());
			fieldInfo.SetValue(target, new Dictionary<string, StringValues>());
		}

		public static void AddCaseInsensitiveHeader(this FrameHeaders target, string key, string value)
		{
			var fieldInfo = GetDictionaryField(target.GetType());
			var values = (Dictionary<string, StringValues>)fieldInfo.GetValue(target);
			values.Add(key, value);
		}

		private static FieldInfo GetDictionaryField(Type headersType)
		{
			var fieldInfo = headersType.GetField("MaybeUnknown", BindingFlags.Instance | BindingFlags.NonPublic);
			if (fieldInfo == null)
			{
				throw new InvalidOperationException("Failed to get field info");
			}

			return fieldInfo;
		}
	}
}
