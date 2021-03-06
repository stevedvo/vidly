﻿using System;
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

		[Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}

			Movie movie = _context.Movies.Include(m => m.Rentals.Select(r => r.Customer)).SingleOrDefault(m => m.Id == id);

			if (movie == null)
			{
				return HttpNotFound();
			}

			EditMovieViewModel viewModel = new EditMovieViewModel(movie)
			{
				Genres = _context.Genres.ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult Edit(EditMovieViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Genres = _context.Genres.ToList();

				return View("Edit", viewModel);
			}

			Movie movie = _context.Movies.SingleOrDefault(m => m.Id == viewModel.Id);

			if (movie == null)
			{
				return HttpNotFound();
			}

			movie.Name = viewModel.Name;
			movie.GenreId = viewModel.GenreId;
			movie.ReleaseDate = viewModel.ReleaseDate;
			movie.StockQuantity = viewModel.StockQuantity;

			_context.SaveChanges();

			return RedirectToAction("Index", "Movie");
		}

		public ActionResult Index()
		{
			if (User.IsInRole(RoleName.CanManageMovies))
			{
				return View("List");
			}

			return View("ListReadOnly");
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

			var movie = _context.Movies.Include(m => m.Genre).Include(m => m.Rentals.Select(r => r.Customer)).SingleOrDefault(m => m.Id == id);

			if (movie == null)
			{
				return HttpNotFound();
			}

			return View(movie);
		}

		[Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult Create()
		{
			var viewModel = new CreateMovieViewModel
			{
				Genres = _context.Genres.ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult Create(CreateMovieViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Genres = _context.Genres.ToList();

				return View("Create", viewModel);
			}

			Movie movie = new Movie
			{
				Name = viewModel.Name,
				GenreId = viewModel.GenreId,
				ReleaseDate = viewModel.ReleaseDate,
				DateAdded = DateTime.Now,
				StockQuantity = viewModel.StockQuantity ?? 0,
				StockAvailable = viewModel.StockQuantity ?? 0
			};

			_context.Movies.Add(movie);
			_context.SaveChanges();

			return RedirectToAction("Index", "Movie");
		}
	}
}