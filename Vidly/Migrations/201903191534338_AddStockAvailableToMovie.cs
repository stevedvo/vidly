namespace Vidly.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class AddStockAvailableToMovie : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.Movies", "StockAvailable", c => c.Int(nullable: false));

			Sql("UPDATE Movies SET StockAvailable = StockQuantity");
		}
		
		public override void Down()
		{
			DropColumn("dbo.Movies", "StockAvailable");
		}
	}
}
