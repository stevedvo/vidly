using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
	public class MovieController : Controller
	{
		// GET: Movie
		public ActionResult Random()
		{
			var movie = new Movie()
			{
				Name = "Shrek!"
			};

			return View(movie);
		}

		public ActionResult Edit(int id)
		{
			return Content("id = " + id);
		}

		public ActionResult Index(int? pageIndex, string sortBy)
		{
			if (!pageIndex.HasValue)
			{
				pageIndex = 1;
			}

			if (string.IsNullOrWhiteSpace(sortBy))
			{
				sortBy = "Name";
			}

			return Content(String.Format($"pageIndex={pageIndex}&sortBy={sortBy}"));
		}

		[Route("Movie/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
		public ActionResult ByReleaseDate(int year, int month)
		{
			return Content(year + "/" + month);
		}
	}
}