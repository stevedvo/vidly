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
	public class RentalsController : ApiController
	{
		private VidlyContext _context = new VidlyContext();

		public RentalsController()
		{
			_context = new VidlyContext();
		}

		//GET /api/rentals
		public IHttpActionResult GetRentals()
		{
			IList<RentalListDTO> rentalListDTOs = new List<RentalListDTO>();

			var rentals = _context.Rentals.Include(r => r.Customer).Include(r => r.Movie).Where(r => r.DateReturned == null).ToList();

			if (rentals.Count > 0)
			{
				foreach (var rental in rentals)
				{
					var rentalListCustomerDTO = new RentalListCustomerDTO
					{
						Id = rental.Customer.Id,
						Name = rental.Customer.Name
					};

					var rentalListMovieDTO = new RentalListMovieDTO
					{
						Id = rental.Movie.Id,
						Name = rental.Movie.Name
					};

					var rentalListDTO = new RentalListDTO
					{
						Id = rental.Id,
						Customer = rentalListCustomerDTO,
						Movie = rentalListMovieDTO,
						DateRented = rental.DateRented,
						DateRentedString = rental.DateRented.ToString("dd MMM yyyy")
					};

					rentalListDTOs.Add(rentalListDTO);
				}
			}

			return Ok(rentalListDTOs);
		}

		//PUT /api/rentals/1
		[HttpPut]
		public string MarkRentalAsReturned(int id)
		{
			var rental = _context.Rentals.SingleOrDefault(r => r.Id == id);

			if (rental == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			var movie = _context.Movies.SingleOrDefault(m => m.Id == rental.MovieId);

			if (movie == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			rental.DateReturned = DateTime.Now;
			movie.StockAvailable++;

			_context.SaveChanges();

			return rental.DateReturned.ToString();
		}
	}
}
