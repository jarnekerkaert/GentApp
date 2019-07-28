using GentApp.Models;
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

		public DbSet<Subscription> Subscriptions { get; set; }

		public DbSet<Event> Events { get; set; }
	}
}
