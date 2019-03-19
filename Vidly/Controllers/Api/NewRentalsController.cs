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

		// GET: /api/newrentals
		public IHttpActionResult GetRentals()
		{
			var rentals = _context.Rentals.ToList();

			return Ok(rentals);
		}

		// POST: /api/newrentals
		[HttpPost]
		public IHttpActionResult CreateNewRentals(NewRentalDTO newRentalDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var customer = _context.Customers.Single(c => c.Id == newRentalDto.CustomerId);

			var movies = _context.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id));

			foreach (var movie in movies)
			{
				var newRental = new Rental
				{
					Customer = customer,
					Movie = movie,
					DateRented = DateTime.Now
				};

				_context.Rentals.Add(newRental);

				movie.StockAvailable--;
			}

			_context.SaveChanges();

			return Ok();
		}
	}
}