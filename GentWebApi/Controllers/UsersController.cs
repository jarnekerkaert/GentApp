using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GentApp.Models;
using GentWebApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GentWebApi.Controllers {
	[Route("api/[controller]")]
	public class UsersController : Controller {

		private readonly GentDbContext _context;
		public UsersController(GentDbContext context) {
			_context = context;
		}

		// GET api/<controller>/5
		[HttpGet("login/{username}")]
		public ActionResult<User> Login(string userName) {
			User response = _context.Users
				.Where(u => u.UserName == userName)
				.SingleOrDefault();
			return response != null ? (ActionResult<User>) response : (ActionResult<User>) NotFound();
		}

		// GET api/<controller>/5
		[HttpGet("{id}")]
		public ActionResult<User> GetById(string id) {
			return _context.Users.Find(id) ?? (ActionResult<User>) NotFound();
		}

		// POST api/<controller>
		[HttpPost("register")]
		public ActionResult<string> Register([FromBody] RegisterModel value) {
			if (ModelState.IsValid) {
				_context.Users
				.Add(new User(value.UserName, value.FirstName, value.LastName, value.Password));
				_context.SaveChanges();
				return Ok();
			}
			else {
				return BadRequest();
			}
		}

		// PUT api/<controller>/5
		[HttpPut("{id}")]
		public IActionResult Put([FromBody]User value) {
			if (ModelState.IsValid) {
				_context.Users
					.Update(value);
				_context.SaveChanges();
				return Ok();
			}
			else {
				return NotFound();
			}
		}

		// DELETE api/<controller>/5
		[HttpDelete("{id}")]
		public IActionResult Delete(User user) {
			if (ModelState.IsValid) {
				_context.Users
					.Remove(user);
				_context.SaveChanges();
				return Ok();
			}
			else {
				return NotFound();
			}
		}
	}
}
