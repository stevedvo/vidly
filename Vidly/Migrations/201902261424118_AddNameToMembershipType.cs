namespace Vidly.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class AddNameToMembershipType : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.MembershipTypes", "name", c => c.String());
			Sql("UPDATE dbo.MembershipTypes SET name = 'Pay As You Go' WHERE Id = 1");
			Sql("UPDATE dbo.MembershipTypes SET name = 'Monthly' WHERE Id = 2");
			Sql("UPDATE dbo.MembershipTypes SET name = 'Quarterly' WHERE Id = 3");
			Sql("UPDATE dbo.MembershipTypes SET name = 'Yearly' WHERE Id = 4");
		}
		
		public override void Down()
		{
			DropColumn("dbo.MembershipTypes", "name");
		}
	}
}
