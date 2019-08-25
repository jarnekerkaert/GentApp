using System.Collections.Generic;
using System.Linq;
using GentWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
		private readonly GentDbContext _context;

		public SubscriptionsController(GentDbContext context)
		{
			_context = context;
		}

        // GET: api/Subscriptions/branch/5
        [HttpGet("branch/{id}", Name = "GetSubscribers")]
        public IEnumerable<Subscription> GetSubscribers(string id)
        {
			return _context.Subscriptions
				.Where(s => s.Branch.Id.Equals(id));
        }

		// GET: api/Subscriptions/user/5
		[HttpGet("user/{id}", Name = "GetSubscriptions")]
		public IEnumerable<Subscription> GetSubscriptions(string id)
		{
			return _context.Subscriptions
				.Where(s => s.UserId.Equals(id)).Include(s => s.Branch);
		}

		// POST: api/Subscriptions
		[HttpPost]
        public IActionResult Post([FromBody] Subscription subscription)
        {
			if (ModelState.IsValid)
			{
				_context.Subscriptions.Add(subscription);
				_context.SaveChanges();
				return Created(subscription.Id, subscription);
			}
			else
			{
				return BadRequest();
			}
		}

		// PUT: api/Subscriptions/5
		[HttpPut("{id}")]
		public IActionResult Put([FromBody] Subscription subscription)
		{
			if (ModelState.IsValid)
			{
				_context.Subscriptions.Update(subscription);
				_context.SaveChanges();
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		// DELETE: api/Subscriptions/5
		[HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
			Subscription subscription = _context.Subscriptions.Find(id);
			if(subscription != null)
			{
				_context.Subscriptions.Remove(subscription);
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
