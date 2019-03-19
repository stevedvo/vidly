using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DAL;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
	public class NewRentalsController : ApiController
	{
		private VidlyContext _context = new VidlyContext();

		public NewRentalsController()
		{
			_context = new VidlyContext();
		}

		// POST: /api/rentals
		[HttpPost]
		public IHttpActionResult CreateNewRentals(NewRentalDTO newRentalDto)
		{
			throw new NotImplementedException();
		}
	}
}