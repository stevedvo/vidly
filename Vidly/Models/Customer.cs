﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
	public class Customer
	{
		public int Id { get; set; }
		[Required]
		[StringLength(255)]
		public string Name { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "Date Of Birth")]
		public DateTime? DateOfBirth { get; set; }
		public bool IsSubscribedToNewsletter { get; set; }
		[Display(Name = "Membership Type")]
		public MembershipType MembershipType { get; set; }
		public byte MembershipTypeId { get; set; }
	}
}