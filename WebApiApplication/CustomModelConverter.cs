using System;
using System.ComponentModel;
using System.Globalization;
using WebApiApplication.Models;

namespace WebApiApplication
{
	public class CustomModelConverter : TypeConverter
	{
		private class ParseResult<T>
		{
			public bool Success { get; set; }

			public Exception Exception { get; set; }

			public T Value { get; set; }
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string s)
			{
				var result = Parse(s);

				if (!result.Success)
				{
					throw new ArgumentException("Invalid format", nameof(value), result.Exception);
				}

				return result.Value;
			}

			return base.ConvertFrom(context, culture, value);
		}

		private ParseResult<CustomModel> Parse(string s)
		{
			if (String.Equals(s, "Success", StringComparison.OrdinalIgnoreCase))
			{
				return new ParseResult<CustomModel>
				{
					Success = true,
					Value = new CustomModel(),
				};
			}

			return new ParseResult<CustomModel>
			{
				Success = false,
				Exception = new FormatException("Bad format")
			};
		}
	}
}
