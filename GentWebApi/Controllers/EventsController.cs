using System;
using System.Collections.Generic;
using System.Linq;
using GentWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
		private readonly GentDbContext _context;

		public EventsController(GentDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Event>> Get() {
			return _context.Events
				.Include(e => e.Branch)
				.ToList();
		}

		[HttpGet("{id}")]
		public ActionResult<IEnumerable<Event>> GetByBranchId(string branchId) {
			return _context.Events.Where(b => b.Branch.Id == branchId).ToList();
		}

		// POST: api/events
		[HttpPost]
		public IActionResult Post([FromBody] Event newEvent)
		{
			if (ModelState.IsValid)
			{
				_context.Events.Add(newEvent);
				_context.SaveChanges();
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		// PUT: api/events/5
		[HttpPut("{id}")]
		public IActionResult Put([FromBody] Event updatedEvent)
		{
			if (ModelState.IsValid)
			{
				_context.Events.Update(updatedEvent);
				_context.SaveChanges();
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		// DELETE: api/events
		[HttpDelete]
		public IActionResult Delete([FromBody] Event ev)
		{
			if (_context.Events.Contains(ev))
			{
				_context.Events.Remove(ev);
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