using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GentApp.Models;
using GentWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GentAppWebApi.Controllers {

	[Route("api/[controller]")]
	[ApiController]
	public class CompaniesController : ControllerBase {

		private readonly GentDbContext _context;

		public CompaniesController(GentDbContext context) {
			_context = context;
			if (_context.Companies.Count() == 0) {
				_context.Companies.Add(new Company { Name = "TasteIt" });
				_context.SaveChanges();
			}
		}

		// GET: api/Companies
		[HttpGet]
		public IEnumerable<Company> Get() {
			return _context.Companies
				.Include(c => c.Branches);
		}

		// GET: api/Companies/5
		[HttpGet("{id}", Name = "Get")]
		public ActionResult<Company> Get(int id) {
			return _context.Companies.Find(id);
		}

		// POST: api/Companies
		[HttpPost]
		public void Post([FromBody] Company company) {
			_context.Companies.Add(company);
		}

		// PUT: api/Companies/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] Company company) {
			_context.Companies.Update(company);
		}

		// DELETE: api/Companies
		[HttpDelete]
		public void Delete([FromBody] Company company) {
			_context.Companies.Remove(company);
		}

		// GET: api/companies/5/branches
		[HttpGet("{id}/branches", Name = "GetBranches")]
		public IEnumerable<Branch> GetBranches(int id)
		{
			//return _context.Companies.Find(id).Branches;
			//return _context.Branches.Where(b => b.CompanyId == id).Include(b => b.Promotions);
			return _context.Branches.Where(b => b.CompanyId == id);
		}

		//// GET: api/companies/5/promotions
		//[HttpGet("{id}/promotions", Name = "GetPromotions")]
		//public IEnumerable<Promotion> GetPromotions(int id)
		//{
		//	return _context.Promotions.Where(b => b.CompanyId == id);
		//}
	}
}
