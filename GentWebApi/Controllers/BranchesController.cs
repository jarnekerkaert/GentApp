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

		[HttpPut("{id}/notifysubscribers")]
		public IActionResult UpdateSubscriptionsEvent([FromBody] bool isEvent, string id)
		{
			var subscriptions = _context.Subscriptions.Where(s => s.Branch.Id.Equals(id));
			foreach (Subscription subscription in subscriptions)
			{
				if (isEvent)
				{
					subscription.AmountEvents++;
				}
				else
				{
					subscription.AmountPromotions++;
				}
			}
			_context.SaveChanges();
			return Ok();
		}

		// GET: api/branches/2/promotions
		[HttpGet("{id}/promotions", Name = "GetPromotions")]
		public IEnumerable<Promotion> GetPromotions(string id)
		{
			return _context.Promotions.Where(p => p.Branch.Id.Equals(id));
		}

		// GET: api/branches/2/events
		[HttpGet("{id}/events", Name = "GetEvents")]
		public IEnumerable<Event> GetEvents(string id)
		{
			return _context.Events.Where(p => p.Branch.Id.Equals(id));
		}

		// DELETE: api/branches
		[HttpDelete]
		public IActionResult Delete([FromBody] Branch branch)
		{
			if (_context.Branches.Contains(branch))
			{
				_context.Branches.Remove(branch);
				List<Subscription> subscriptionsToDelete = _context.Subscriptions.Where(s => s.BranchId.Equals(branch.Id)).ToList();
				subscriptionsToDelete.ForEach(s => _context.Subscriptions.Remove(s));
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