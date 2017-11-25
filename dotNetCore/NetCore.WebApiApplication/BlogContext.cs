using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NetCore.WebApiApplication
{
    public class BlogContext : DbContext
    {
	    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
	    {
	    }
    }
}
