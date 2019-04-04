using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
	public class Customer
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter customer's name")]
		[StringLength(255)]
		public string Name { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "Date Of Birth")]
		[Min18YearsIfAMember]
		public DateTime? DateOfBirth { get; set; }

		[Display(Name = "Subscribed to Newsletter")]
		public bool IsSubscribedToNewsletter { get; set; }

		[Display(Name = "Membership Type")]
		public MembershipType MembershipType { get; set; }

		[Display(Name = "Membership Type")]
		public byte MembershipTypeId { get; set; }

		public IList<Rental> Rentals { get; set; }

		public bool Deleted { get; set; }

		public int CurrentRentalTotal
		{
			get
			{
				if (Rentals == null)
				{
					Rentals = new List<Rental>();
				}

				var currentRentalTotal = Rentals.Where(x => x.DateReturned == null).Count();

				return currentRentalTotal;
			}
		}

		public bool BlackFlag { get; set; }
	}
}