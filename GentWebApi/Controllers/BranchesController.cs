﻿using System;
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
			if (_context.Branches.Count() == 0)
			{
				_context.Branches.Add(new Branch { Name = "TasteIt" });
				_context.SaveChanges();
			}
		}

		// GET: api/branches/2/promotions
		[HttpGet("{id}/promotions", Name = "GetPromotions")]
		public IEnumerable<Promotion> GetPromotions(int id)
		{
			return _context.Promotions.Where(p => p.BranchId == id);
		}

		// PUT: api/branches/2
		[HttpPut("{id}")]
		public HttpResponseMessage Put(int id, [FromBody] Branch branch)
		{
			if (this.ModelState.IsValid)
			{
				_context.Branches.Update(branch);
				_context.SaveChanges();
				return new HttpResponseMessage(HttpStatusCode.OK);
			}
			else
			{
				return new HttpResponseMessage(HttpStatusCode.BadRequest);
			}
		}
	}
}