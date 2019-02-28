using AutoMapper;
using System;
using System.Collections.Generic;
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
		public IEnumerable<MovieDTO> GetMovies()
		{
			return _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDTO>);
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
		public IHttpActionResult CreateMovie(MovieDTO movieDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var movie = Mapper.Map<MovieDTO, Movie>(movieDto);
			movie.DateAdded = DateTime.Now;

			_context.Movies.Add(movie);
			_context.SaveChanges();

			movieDto.Id = movie.Id;
			movieDto.DateAdded = movie.DateAdded;

			return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
		}

		//PUT /api/movies/1
		[HttpPut]
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
		public void DeleteMovie(int id)
		{
			var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

			if (movie == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			_context.Movies.Remove(movie);
			_context.SaveChanges();
		}
	}
}
