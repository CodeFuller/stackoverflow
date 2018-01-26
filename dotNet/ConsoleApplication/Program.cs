using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ConsoleApplication.Dal;

namespace ConsoleApplication
{
	public interface IRepository
	{
	}

	public class ComputedColumns : IRepository
	{
	}

	public class DataRepository : IRepository
	{
	}

	public interface ICacheService<TKey, TValue>
	{
	}

	public class MyData
	{
	}

	public class MyDataCache : IRepository, ICacheService<int, List<MyData>>
	{
	}

	public class Installer<TDataRepository> : IWindsorInstaller where TDataRepository : IRepository
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<IRepository, ICacheService<int, List<MyData>>>().ImplementedBy<MyDataCache>());
			container.Register(Component.For<IRepository>().ImplementedBy<ComputedColumns>());
			container.Register(Component.For<IRepository>().ImplementedBy<TDataRepository>());
		}
	}

	public class MockedRepository : IRepository
	{
	}

	class Program
	{
		static void Main(string[] args)
		{
			var container = new WindsorContainer();

			//	In production code
			container.Install(new Installer<DataRepository>());

			//	In integration test
			container.Install(new Installer<MockedRepository>());
		}
	}
}
