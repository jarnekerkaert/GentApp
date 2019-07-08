using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GentApp.Models;

using GentWebApi.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GentWebApi {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddDbContext<GentDbContext>(opt =>
				opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

			//services.AddDefaultIdentity<User>(o => {
			//	o.Password.RequireDigit = false;
			//	o.Password.RequireLowercase = false;
			//	o.Password.RequireNonAlphanumeric = false;
			//	o.Password.RequireUppercase = false;
			//	o.Password.RequiredLength = 2;
			//})
			//	.AddEntityFrameworkStores<GentDbContext>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if ( env.IsDevelopment() ) {
				app.UseDeveloperExceptionPage();
			}
			else {
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				//app.UseHsts();
			}

			//app.UseHttpsRedirection();
			//app.UseAuthentication();
			app.UseMvc();
		}
	}
}
