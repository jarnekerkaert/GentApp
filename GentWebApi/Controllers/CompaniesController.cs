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
			if (ModelState.IsValid) {
				return _context.Companies.Find(id);
			}
			else {
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
				return NotFound();
			}
		}

		// DELETE: api/Companies
		[HttpDelete]
		public IActionResult Delete([FromBody] Company company) {
			if (ModelState.IsValid) {
				_context.Companies.Remove(company);
				_context.SaveChanges();
				return Ok();
			}
			else {
				return NotFound();
			}
		}
	}
}
