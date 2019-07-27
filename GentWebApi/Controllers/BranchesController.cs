using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GentApp.Models;
using GentWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
		private readonly GentDbContext _context;

		public BranchesController(GentDbContext context)
		{
			_context = context;
		}

		// GET api/branches
		[HttpGet]
		public ActionResult<IEnumerable<Branch>> Get() {
			return _context.Branches;
		}

		// GET api/branches/2
		[HttpGet("{id}")]
		public ActionResult<Branch> Get(string id) {
			if (_context.Branches.Find(id) != null)
			{
				return _context.Branches.Find(id);
			}
			else
			{
				return NotFound();
			}
		}

		// POST: api/branches
		[HttpPost]
		public IActionResult Post([FromBody] Branch branch)
		{
			if (ModelState.IsValid)
			{
				_context.Branches.Add(branch);
				_context.SaveChanges();
				return Created(branch.Id, branch);
			}
			else
			{
				return BadRequest();
			}
		}

		// PUT: api/branches/2
		[HttpPut("{id}")]
		public IActionResult Put([FromBody] Branch branch, string id)
		{
			//if (_context.Branches.Find(id) == null)
			//{
			//	return NotFound();
			//}
			if (ModelState.IsValid)
			{
				_context.Branches.Update(branch);
				_context.SaveChanges();
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		// GET: api/branches/2/promotions
		[HttpGet("{id}/promotions", Name = "GetPromotions")]
		public IEnumerable<Promotion> GetPromotions(string id)
		{
			//return _context.Promotions.Where(p => p.Branch.Id.Equals(id));
			return _context.Promotions.Where(p => p.BranchId.Equals(id));
		}

		// GET: api/branches/2/events
		[HttpGet("{id}/events", Name = "GetEvents")]
		public IEnumerable<Event> GetEvents(string id)
		{
			//return _context.Promotions.Where(p => p.Branch.Id.Equals(id));
			return _context.Events.Where(p => p.BranchId.Equals(id));
		}

		// DELETE: api/branches
		[HttpDelete]
		public IActionResult Delete([FromBody] Branch branch)
		{
			if (_context.Branches.Contains(branch))
			{
				_context.Branches.Remove(branch);
				_context.SaveChanges();
				return Ok();
			}
			else
			{
				return NotFound();
			}
		}
	}
}