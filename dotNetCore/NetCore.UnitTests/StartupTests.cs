using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetCore.WebApiApplication;
using NetCore.WebApiApplication.Controllers;

namespace NetCore.UnitTests
{
	[TestClass]
	public class StartupTests
	{
		[TestMethod]
		public void ConfigureServices_RegistersDependenciesCorrectly()
		{
			//	Arrange

			//	Setting up stuff required for Configuration.GetConnectionString("DefaultConnection")
			Mock<IConfigurationSection> configurationSectionStub = new Mock<IConfigurationSection>();
			configurationSectionStub.Setup(x => x["DefaultConnection"]).Returns("TestConnectionString");
			Mock<Microsoft.Extensions.Configuration.IConfiguration> configurationStub = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
			configurationStub.Setup(x => x.GetSection("ConnectionStrings")).Returns(configurationSectionStub.Object);

			IServiceCollection services = new ServiceCollection();
			var target = new Startup(configurationStub.Object);

			//	Act

			target.ConfigureServices(services);
			//	Mimic internal asp.net core logic.
			services.AddTransient<TestController>();

			//	Assert

			var serviceProvider = services.BuildServiceProvider();

			var controller = serviceProvider.GetService<TestController>();
			Assert.IsNotNull(controller);
		}
	}
}
