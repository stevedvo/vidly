using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.DTOs
{
	public class RentalListDTO
	{
		public int Id { get; set; }
		public RentalListCustomerDTO Customer { get; set; }
		public RentalListMovieDTO Movie { get; set; }

		public DateTime DateRented { get; set; }
		public DateTime? DateReturned { get; set; }

		public string DateRentedString { get; set; }
	}
}