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
		}

		// GET: api/Companies
		[HttpGet]
		public IEnumerable<Company> Get() {
			return _context.Companies
				.Include(c => c.Branches);
		}

		// GET: api/Companies/5
		[HttpGet("{id}", Name = "Get")]
		public ActionResult<Company> Get(string id) {
			if (_context.Companies.Find(id) != null)
			{
				//return _context.Companies.Find(id);
				return _context.Companies.Include(c => c.Branches).Where(c => c.Id.Equals(id)).FirstOrDefault();
			}
			else
			{
				return NotFound();
			}
		}

		// POST: api/Companies
		[HttpPost]
		public IActionResult Post([FromBody] Company company) {
			if (ModelState.IsValid) {
				_context.Companies
				.Add(company);
				_context.SaveChanges();
				return Created(company.Id, company);
			}
			else {
				return BadRequest();
			}
		}

		// PUT: api/Companies/5
		[HttpPut("{id}")]
		public IActionResult Put([FromBody] Company company) {
			if (ModelState.IsValid) {
				_context.Companies.Update(company);
				_context.SaveChanges();
				return Ok();
			}
			else {
				return BadRequest();
			}
		}

		// DELETE: api/Companies
		[HttpDelete]
		public IActionResult Delete([FromBody] Company company) {
			if (_context.Companies.Contains(company)) {
				_context.Companies.Remove(company);
				_context.SaveChanges();
				return Ok();
			}
			else {
				return NotFound();
			}
		}

		// GET: api/companies/5/branches
		[HttpGet("{id}/branches", Name = "GetBranches")]
		public IEnumerable<Branch> GetBranches(string id)
		{
			//return _context.Companies.Find(id).Branches;
			//return _context.Branches.Where(b => b.CompanyId == id).Include(b => b.Promotions);
			///werkt: 
			return _context.Branches.Where(b => b.CompanyId.Equals(id));
			//return _context.Branches.Where(b => b.CompanyId.Equals(id)).Include(b => b.Promotions);
		}
	}
}
