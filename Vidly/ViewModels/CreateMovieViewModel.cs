using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
	public class CreateMovieViewModel
	{
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
		[Display(Name = "Number in Stock")]
		public int? StockQuantity { get; set; }
	}
}