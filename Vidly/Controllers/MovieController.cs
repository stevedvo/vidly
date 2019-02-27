using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.DAL;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
	public class MovieController : Controller
	{
		private VidlyContext _context = new VidlyContext();

		public MovieController()
		{
			_context = new VidlyContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET: Movie
		public ActionResult Random()
		{
			var movie = new Movie()
			{
				Name = "Shrek!"
			};

			var customers = new List<Customer>
			{
				new Customer {Name = "Customer 1"},
				new Customer {Name = "Customer 2"},
				new Customer {Name = "Customer 3"},
				new Customer {Name = "Customer 4"},
				new Customer {Name = "Customer 5"},
				new Customer {Name = "Customer 6"}
			};

			var viewModel = new RandomMovieViewModel
			{
				Movie = movie,
				Customers = customers
			};

			return View(viewModel);
		}

		public ActionResult Edit(int id)
		{
			return Content("id = " + id);
		}

		public ActionResult Index(int? pageIndex, string sortBy)
		{
			var movies = _context.Movies.Include(m => m.Genre).ToList();

			return View(movies);

			//if (!pageIndex.HasValue)
			//{
			//	pageIndex = 1;
			//}

			//if (string.IsNullOrWhiteSpace(sortBy))
			//{
			//	sortBy = "Name";
			//}

			////return Content(String.Format($"pageIndex={pageIndex}&sortBy={sortBy}"));

			//return View(movies);
		}

		[Route("Movie/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
		public ActionResult ByReleaseDate(int year, int month)
		{
			return Content(year + "/" + month);
		}

		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}

			var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

			if (movie == null)
			{
				return HttpNotFound();
			}

			return View(movie);
		}
	}
}