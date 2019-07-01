using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GentApp.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GentWebApi.Models {
	public class GentDbContext : IdentityDbContext<User> {
		public GentDbContext(DbContextOptions<GentDbContext> options)
				: base(options) {
		}

		public DbSet<Company> Companies { get; set; }
	}
}
