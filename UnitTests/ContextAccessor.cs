using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace UnitTests
{
	internal static class ContextAccessor
	{
		private static TestExecutionContext currentRequestTest;

		private static RequestContext currentRequestContext;

		public static RequestContext Current
		{
			get
			{
				var currTest = TestExecutionContext.CurrentContext;

				if (currentRequestTest == currTest)
				{
					return currentRequestContext;
				}

				currentRequestContext = CreateRequestContext();
				currentRequestTest = currTest;

				return currentRequestContext;
			}
		}

		public static RequestContext CreateRequestContext()
		{
			return new RequestContext();
		}
	}
}
