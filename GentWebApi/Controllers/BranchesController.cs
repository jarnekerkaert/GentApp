using System;
using System.Collections.Generic;
using System.Linq;
using GentApp.Models;
using GentWebApi.Models;
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
			return _context.Branches
				.Include(b => b.Events)
				.Include(b => b.Promotions)
				.ToList();
		}

		// GET api/branches/2
		[HttpGet("{id}")]
		public ActionResult<Branch> Get(string id) {
			var result = _context.Branches.Find(id);
			return result != null ? (ActionResult<Branch>) result : (ActionResult<Branch>) NotFound();
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