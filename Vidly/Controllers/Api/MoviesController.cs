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
	public class MoviesController : ApiController
	{
		private VidlyContext _context = new VidlyContext();

		public MoviesController()
		{
			_context = new VidlyContext();
		}

		//GET /api/movies
		public IHttpActionResult GetMovies(string query = null, bool? allstock = true)
		{
			var moviesQuery = _context.Movies.Include(m => m.Genre).Include(m => m.Rentals).Where(m => m.Deleted == false);

			if (allstock == false)
			{
				moviesQuery = moviesQuery.Where(m => m.StockAvailable > 0);
			}

			if (!String.IsNullOrWhiteSpace(query))
			{
				moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
			}

			var movieDtos = moviesQuery.ToList().Select(Mapper.Map<Movie, MovieDTO>);

			return Ok(movieDtos);
		}

		//GET /api/movies/1
		public IHttpActionResult GetMovie(int id)
		{
			var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

			if (movie == null)
			{
				return NotFound();
			}

			return Ok(Mapper.Map<Movie, MovieDTO>(movie));
		}

		//POST /api/movies
		[HttpPost]
		[Authorize(Roles = RoleName.CanManageMovies)]
		public IHttpActionResult CreateMovie(MovieDTO movieDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var movie = Mapper.Map<MovieDTO, Movie>(movieDto);
			movie.DateAdded = DateTime.Now;
			movie.StockAvailable = movie.StockQuantity;

			_context.Movies.Add(movie);
			_context.SaveChanges();

			movieDto.Id = movie.Id;
			movieDto.DateAdded = movie.DateAdded;

			return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
		}

		//PUT /api/movies/1
		[HttpPut]
		[Authorize(Roles = RoleName.CanManageMovies)]
		public void UpdateMovie(int id, MovieDTO movieDto)
		{
			if (!ModelState.IsValid)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

			if (movie == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			Mapper.Map(movieDto, movie);

			_context.SaveChanges();
		}

		//DELETE /api/movies/1
		[HttpDelete]
		[Authorize(Roles = RoleName.CanManageMovies)]
		public bool DeleteMovie(int id)
		{
			var movie = _context.Movies.Include(m => m.Rentals).SingleOrDefault(m => m.Id == id);

			if (movie == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			bool canDeleteMovie = true;

			if (movie.Rentals.Count > 0)
			{
				foreach (var rental in movie.Rentals)
				{
					if (canDeleteMovie)
					{
						if (rental.DateReturned == null)
						{
							canDeleteMovie = false;
						}
					}
				}
			}

			if (canDeleteMovie)
			{
				movie.Deleted = true;
				_context.SaveChanges();
			}

			return canDeleteMovie;
		}
	}
}
