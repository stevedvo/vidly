using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
	public class Movie
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Genre Genre { get; set; }
		public int? GenreId { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dddd, MMMM d, yyyy}", ApplyFormatInEditMode = true)]
		[Display(Name = "Release Date")]
		[Required]
		public DateTime? ReleaseDate { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dddd, MMMM d, yyyy}", ApplyFormatInEditMode = true)]
		[Display(Name = "Date Added")]
		public DateTime? DateAdded { get; set; }

		[Display(Name = "Number in Stock")]
		[Required]
		[Range(1, 20)]
		public int StockQuantity { get; set; }
	}
}