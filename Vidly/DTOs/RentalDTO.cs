using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.DTOs
{
	public class RentalDTO
	{
		public int Id { get; set; }
		public DateTime DateRented { get; set; }
		public DateTime? DateReturned { get; set; }
		public int CustomerId { get; set; }
		public int MovieId { get; set; }
	}
}