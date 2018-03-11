using System.IO;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace NetCore.ConsoleApplication
{
	public class SomeSettings
	{
		public string SomeValue { get; set; }
	}

	class Program
	{
		static void Main(string[] args)
		{
			var services = new ServiceCollection();
			services.AddDataProtection()
				.PersistKeysToFileSystem(new DirectoryInfo(@"c:\temp\"))
				// or .PersistKeysToRegistry(Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Sample\keys"))
				.ProtectKeysWithDpapi();

			var serviceProvider = services.BuildServiceProvider();
			var options = serviceProvider.GetService<IOptions<KeyManagementOptions>>();
			var keyManagementOptions = options.Value;

			var xmlRepository = keyManagementOptions.XmlRepository;
			//	Microsoft.AspNetCore.DataProtection.Repositories.FileSystemXmlRepository
			var repositoryType = xmlRepository?.GetType();

			//	Microsoft.AspNetCore.DataProtection.XmlEncryption.DpapiXmlEncryptor
			var xmlEncryptor = keyManagementOptions.XmlEncryptor;
			var encryptorType = xmlEncryptor?.GetType();
		}
	}
}
