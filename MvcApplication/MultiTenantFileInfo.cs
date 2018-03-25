using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.FileProviders;

namespace MvcApplication
{
	public class MultiTenantFileInfo : IFileInfo, IDirectoryContents
	{
		private const string BasePath = @"DomainsData";

		private readonly bool forceNotExists;

		private string FullPath { get; }

		public bool Exists => !forceNotExists && (File.Exists(FullPath) || Directory.Exists(FullPath));

		public long Length => File.Exists(FullPath) ? new FileInfo(FullPath).Length : -1;

		public string PhysicalPath => FullPath;

		public string Name => Path.GetFileName(FullPath);

		public DateTimeOffset LastModified => new FileInfo(FullPath).LastWriteTime;

		public bool IsDirectory => Directory.Exists(FullPath);

		public MultiTenantFileInfo(string tenantId, string subpath)
		{
			subpath = subpath.TrimStart(Path.AltDirectorySeparatorChar);
			subpath = subpath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			FullPath = Path.Combine(BasePath, tenantId, subpath);
		}

		public static MultiTenantFileInfo NonExistingFile(string subPath)
		{
			return new MultiTenantFileInfo(subPath, true);
		}

		private MultiTenantFileInfo(string fullPath, bool forceNotExists)
		{
			FullPath = fullPath;
			this.forceNotExists = forceNotExists;
		}

		public Stream CreateReadStream()
		{
			return File.OpenRead(FullPath);
		}

		public IEnumerator<IFileInfo> GetEnumerator()
		{
			return Directory.EnumerateFiles(FullPath).Select(filePath => new MultiTenantFileInfo(filePath, false)).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
