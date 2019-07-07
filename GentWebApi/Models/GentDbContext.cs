using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GentApp.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GentWebApi.Models {
	public class GentDbContext : DbContext {
		public GentDbContext(DbContextOptions<GentDbContext> options)
				: base(options) {
		}

		public DbSet<Company> Companies { get; set; }

		public DbSet<Branch> Branches { get; set; }

		public DbSet<Promotion> Promotions { get; set; }

		public DbSet<User> Users { get; set; }

	}
}
