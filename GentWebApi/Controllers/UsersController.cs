﻿using System;
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
		[HttpGet("{id}")]
		public ActionResult<User> Get(string id) {
			if (_context.Users.Find(id) != null)
			{
				return _context.Users.Find(id);
			}
			else
			{
				return NotFound();
			}
		}

		// POST api/<controller>
		[HttpPost]
		public IActionResult Post([FromBody] User value) {
			if (ModelState.IsValid) {
				_context.Users
				.Add(value);
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