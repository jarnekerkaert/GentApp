using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GentApp.Models;

using GentWebApi.Models;
using MetroLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// Used http://bitoftech.net/2015/01/21/asp-net-identity-2-with-asp-net-web-api-2-accounts-management/

namespace GentWebApi.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase {
		private UserManager<User> _userManager;
		private SignInManager<User> _signInManager;
		private ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<AccountController>();

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager) {
			_userManager = userManager;
			_signInManager = signInManager;
		}

		// GET: api/Account
		[HttpGet]
		public string Get() {
			return "value1";
		}

		// GET: api/Account/5
		[HttpGet("{id}", Name = "GetUserById")]
		public async Task<IActionResult> Get(string id) {
			var user = await _userManager.FindByIdAsync(id);

			if ( user != null ) {
				return Ok(user);
			}

			return NotFound();
		}

		// POST: api/Account
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] RegisterModel registerModel) {
			User user = new User(registerModel.UserName);
			var result = await _userManager.CreateAsync(user, registerModel.Password);
			if ( result.Succeeded ) {
				await _signInManager.SignInAsync(user, isPersistent: false);

				Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));
				return Created(locationHeader, user);
			}

			log.Error(result.Errors.FirstOrDefault().Description);
			return BadRequest(result.Errors.FirstOrDefault().Description);
		}

		// PUT: api/Account/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value) {
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public void Delete(int id) {
		}
	}
}
