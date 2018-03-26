using System;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Internal;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Primitives;

namespace MvcApplication
{
	public class MultiTenantFileProvider : IFileProvider
	{
		private const string BasePath = @"DomainsData";

		public IFileInfo GetFileInfo(string subpath)
		{
			if (MultiTenantHelper.CurrentHttpContext == null)
			{
				if (String.Equals(subpath, @"/Pages/_ViewImports.cshtml") || String.Equals(subpath, @"/_ViewImports.cshtml"))
				{
					//	Return FileInfo of non-existing file.
					return new NotFoundFileInfo(subpath);
				}

				throw new InvalidOperationException("HttpContext is not set");
			}

			return CreateFileInfoForCurrentRequest(subpath);
		}

		public IDirectoryContents GetDirectoryContents(string subpath)
		{
			var fullPath = GetPhysicalPath(MultiTenantHelper.CurrentRequestDomain, subpath);
			return new PhysicalDirectoryContents(fullPath);
		}

		public IChangeToken Watch(string filter)
		{
			return NullChangeToken.Singleton;
		}

		private IFileInfo CreateFileInfoForCurrentRequest(string subpath)
		{
			var fullPath = GetPhysicalPath(MultiTenantHelper.CurrentRequestDomain, subpath);
			return new PhysicalFileInfo(new FileInfo(fullPath));
		}

		private string GetPhysicalPath(string tenantId, string subpath)
		{
			subpath = subpath.TrimStart(Path.AltDirectorySeparatorChar);
			subpath = subpath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			return Path.Combine(BasePath, tenantId, subpath);
		}
	}
}
