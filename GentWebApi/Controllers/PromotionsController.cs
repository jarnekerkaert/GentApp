using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GentWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
		private readonly GentDbContext _context;

		public PromotionsController(GentDbContext context)
		{
			_context = context;
		}

		// GET: api/promotions
		[HttpGet]
		public ActionResult<IEnumerable<Promotion>> GetAll()
		{
				return _context.Promotions;
		}

		// GET: api/promotions/5
		[HttpGet("{id}", Name = "GetPromotion")]
        public ActionResult<Promotion> GetPromotion(string id)
        {
			if (_context.Promotions.Find(id) != null)
			{
				return _context.Promotions.Find(id);
			}
			else
			{
				return NotFound();
			}
		}

        // POST: api/promotions
        [HttpPost]
        public IActionResult Post([FromBody] Promotion promotion)
        {
			if (ModelState.IsValid)
			{
				_context.Promotions.Add(promotion);
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		// PUT: api/promotions/5
		[HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Promotion promotion)
        {
			if (ModelState.IsValid)
			{
				_context.Promotions.Update(promotion);
				_context.SaveChanges();
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

        // DELETE: api/promotions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
			Promotion promotion = _context.Promotions.Find(id);
			if (promotion != null)
			{
				_context.Promotions.Remove(promotion);
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
