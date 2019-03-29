using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DAL;

namespace Vidly.Controllers.Api
{
	public class RentalsController : ApiController
	{
		private VidlyContext _context = new VidlyContext();

		public RentalsController()
		{
			_context = new VidlyContext();
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
