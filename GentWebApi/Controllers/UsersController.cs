using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GentApp.Models;
using GentWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
			string authHeader = HttpContext.Request.Headers["Authorization"];
			string username = "";
			string password = "";
			if ( authHeader?.StartsWith("Basic") == true )
			{
				string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
				byte[] usernamePasswordBytes = Convert.FromBase64String(encodedUsernamePassword);
				string usernamePassword = System.Text.Encoding.ASCII.GetString(usernamePasswordBytes);
				int index = usernamePassword.IndexOf(':');
				username = usernamePassword.Substring(0, index);
				password = usernamePassword.Substring(index + 1);

				byte[] decryptedPasswordBytes = System.Text.Encoding.UTF8.GetBytes(password);
				password = Convert.ToBase64String(decryptedPasswordBytes);

			}
			else
			{
				return BadRequest("The authorization header is either empty or isn't Basic.");
			}

			if (!username.Equals("") && !password.Equals(""))
			{
				User response = _context.Users
					.Include(u => u.Company)
					.ThenInclude(c => c.Branches)
					.ThenInclude(b => b.Events)
					.Include(u => u.Company)
					.ThenInclude(c => c.Branches)
					.ThenInclude(b => b.Promotions)
					.Where(u => u.UserName == userName)
					.SingleOrDefault();

				if(response == null)
				{
					return NotFound("There is not an account in our database with given username and password.");
				}

				if(response.UserName.Equals(username) && response.Password.Equals(password))
				{
					return response;
				}
				else
				{
					return Unauthorized("Bad credentials");
				}
			}
			else
			{
				return BadRequest("Username and password in the authorization header can't be empty.");
			}
		}

		// GET api/<controller>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetById(string id) {
			return await _context.Users.FindAsync(id);
		}

		// GET api/<controller>/5
		[HttpGet("checkuser/{name}")]
		public async Task<ActionResult<User>> CheckUsername(string name) {
			var result = await _context.Users.Where(u => u.UserName.Equals(name)).FirstOrDefaultAsync();
			if ( result != null ) {
				return result;
			}
			else {
				return NotFound();
			}
		}

		// POST api/<controller>
		[HttpPost("register")]
		public ActionResult<string> Register([FromBody] RegisterModel user) {
			if ( ModelState.IsValid ) {
				User newUser = new User(user.UserName, user.FirstName, user.LastName, user.Password);
				_context.Users
				.Add(newUser);
				_context.SaveChangesAsync();
				return Created(newUser.Id, newUser);
			}
			else {
				return BadRequest();
			}
		}

		// PUT api/<controller>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(string id, [FromBody]User value) {
			if ( ModelState.IsValid ) {
				_context.Users.Update(value);
				await _context.SaveChangesAsync();
				return Ok();
			}
			else {
				return BadRequest();
			}
		}

		// DELETE api/<controller>/5
		[HttpDelete("{id}")]
		public IActionResult Delete(User user) {
			if ( ModelState.IsValid ) {
				_context.Users
					.Remove(user);
				_context.SaveChanges();
				return Ok();
			}
			else {
				return BadRequest();
			}
		}

		[HttpGet("{id}/subscribedbranches")]
		public IEnumerable<Branch> GetSubscribedBranchesOfUser(string id) {
			List<Subscription> subscriptions = _context.Subscriptions.Where(s => s.UserId.Equals(id)).Include(s => s.Branch).ToList();
			List<Branch> branches = new List<Branch>();
			foreach ( Subscription subscription in subscriptions ) {
				branches.Add(
					_context.Branches
					.Include(b => b.Events)
					.Include(b => b.Promotions)
					.FirstOrDefault(b => b.Id == subscription.Branch.Id));
			}
			return branches;
		}
	}
}
