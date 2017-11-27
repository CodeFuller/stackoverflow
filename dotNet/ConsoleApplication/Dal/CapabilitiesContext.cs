using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication.Objects;

namespace ConsoleApplication.Dal
{
	public partial class Resource
	{
		public int Id { get; set; }
		public string Description { get; set; }

		public virtual ICollection<ResourceCapability> ResourceCapability { get; set; }
	}

	public partial class Capability
	{
		public int Id { get; set; }
		public string Description { get; set; }

		public virtual ICollection<ResourceCapability> ResourceCapability { get; set; }

	}

	public partial class ResourceCapability
	{
		public int Id { get; set; }
		public int ResourceId { get; set; }
		public int CapabilityId { get; set; }

		public virtual Resource Resource { get; set; }
		public virtual Capability Capability { get; set; }
	}

	public class CapabilitiesContext : DbContext
	{
		public DbSet<Resource> Resources { get; set; }

		public DbSet<Capability> Capabilities { get; set; }

		public DbSet<ResourceCapability> ResourceCapabilities { get; set; }

		public CapabilitiesContext() :
			base("name=CapabilitiesContext")
		{
		}
	}
}
