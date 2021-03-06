﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.DTOs
{
	public class CustomerDTO
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter customer's name")]
		[StringLength(255)]
		public string Name { get; set; }

		//[Min18YearsIfAMember]
		public DateTime? DateOfBirth { get; set; }

		public bool IsSubscribedToNewsletter { get; set; }

		public byte MembershipTypeId { get; set; }

		public MembershipTypeDTO MembershipType { get; set; }

		public IList<RentalDTO> Rentals { get; set; }

		public int CurrentRentalTotal { get; set; }

		public bool BlackFlag { get; set; }
	}
}