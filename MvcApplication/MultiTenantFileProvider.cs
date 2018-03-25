using System;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace MvcApplication
{
	public class MultiTenantFileProvider : IFileProvider
	{
		public IFileInfo GetFileInfo(string subpath)
		{
			if (MultiTenantHelper.CurrentHttpContext == null)
			{
				if (String.Equals(subpath, @"/Pages/_ViewImports.cshtml") || String.Equals(subpath, @"/_ViewImports.cshtml"))
				{
					//	Return FileInfo of non-existing file.
					return MultiTenantFileInfo.NonExistingFile(subpath);
				}

				throw new InvalidOperationException("HttpContext is not set");
			}

			return CreateFileInfoForCurrentRequest(subpath);
		}

		public IDirectoryContents GetDirectoryContents(string subpath)
		{
			return CreateFileInfoForCurrentRequest(subpath);
		}

		public IChangeToken Watch(string filter)
		{
			return new NoChangeToken();
		}

		private MultiTenantFileInfo CreateFileInfoForCurrentRequest(string subpath)
		{
			return new MultiTenantFileInfo(MultiTenantHelper.CurrentRequestDomain, subpath);
		}
	}
}
