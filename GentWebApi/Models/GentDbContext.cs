using GentApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GentWebApi.Models {
	public class GentDbContext : DbContext {
		public GentDbContext(DbContextOptions<GentDbContext> options)
				: base(options) {
		}

		public DbSet<Company> Companies { get; set; }

		public DbSet<Branch> Branches { get; set; }

	}
}
