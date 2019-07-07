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
		[HttpGet("{id}")]
		public ActionResult<IEnumerable<Branch>> Get() {
			return _context.Branches;
		}

		// GET api/branches/2
		[HttpGet("{id}")]
		public ActionResult<Branch> Get(string id) {
			if ( ModelState.IsValid ) {
				return _context
				.Branches
				.Find(id);
			}
			else {
				return NotFound();
			}
		}

		// PUT: api/branches/2
		[HttpPut("{id}")]
		public IActionResult Put([FromBody] Branch branch)
		{
			if (ModelState.IsValid)
			{
				_context.Branches.Update(branch);
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