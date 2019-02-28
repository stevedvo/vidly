using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
	public class Min18YearsIfAMember : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var customer = (Customer)validationContext.ObjectInstance;

			if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
			{
				return ValidationResult.Success;
			}

			if (customer.DateOfBirth == null)
			{
				return new ValidationResult("Birthdate is required.");
			}

			var age = DateTime.Today.Year - customer.DateOfBirth.Value.Year;

			return (age < 18 ? new ValidationResult("Customer should be at least 18 years of age for this Membership Type") : ValidationResult.Success);
		}
	}
}