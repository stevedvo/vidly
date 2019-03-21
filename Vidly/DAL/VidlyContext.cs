using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.DAL
{
	public class VidlyContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<MembershipType> MembershipTypes { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Rental> Rentals { get; set; }

		public VidlyContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public static VidlyContext Create()
		{
			return new VidlyContext();
		}
	}
}

