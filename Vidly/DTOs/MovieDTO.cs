using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.DTOs
{
	public class MovieDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int? GenreId { get; set; }

		[Required]
		public DateTime? ReleaseDate { get; set; }

		public DateTime? DateAdded { get; set; }

		[Required]
		[Range(1, 20)]
		public int StockQuantity { get; set; }

		public GenreDTO Genre { get; set; }
	}
}