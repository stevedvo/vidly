using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Vidly.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<Vidly.DAL.VidlyContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		} 
	}
}