using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
	public class EditMovieViewModel
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Genre")]
		public int? GenreId { get; set; }

		public IEnumerable<Genre> Genres { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "Release Date")]
		public DateTime? ReleaseDate { get; set; }

		[Required]
		[Range(1, 20)]
		[Display(Name = "Stock Total")]
		public int StockQuantity { get; set; }

		[Display(Name = "Stock Available")]
		[Required]
		[Range(0, 20)]
		public int StockAvailable { get; set; }

		public EditMovieViewModel()
		{

		}

		public EditMovieViewModel(Movie movie)
		{
			Id = movie.Id;
			Name = movie.Name;
			ReleaseDate = movie.ReleaseDate;
			GenreId = movie.GenreId;
			StockQuantity = movie.StockQuantity;
			StockAvailable = movie.StockAvailable;
		}
	}
}