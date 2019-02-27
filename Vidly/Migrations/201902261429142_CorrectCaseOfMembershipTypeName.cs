namespace Vidly.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class CorrectCaseOfMembershipTypeName : DbMigration
	{
		public override void Up()
		{
			DropColumn("dbo.MembershipTypes", "name");
			AddColumn("dbo.MembershipTypes", "Name", c => c.String());
			Sql("UPDATE dbo.MembershipTypes SET Name = 'Pay As You Go' WHERE Id = 1");
			Sql("UPDATE dbo.MembershipTypes SET Name = 'Monthly' WHERE Id = 2");
			Sql("UPDATE dbo.MembershipTypes SET Name = 'Quarterly' WHERE Id = 3");
			Sql("UPDATE dbo.MembershipTypes SET Name = 'Yearly' WHERE Id = 4");
		}

		public override void Down()
		{
			DropColumn("dbo.MembershipTypes", "Name");
			AddColumn("dbo.MembershipTypes", "name", c => c.String());
			Sql("UPDATE dbo.MembershipTypes SET name = 'Pay As You Go' WHERE Id = 1");
			Sql("UPDATE dbo.MembershipTypes SET name = 'Monthly' WHERE Id = 2");
			Sql("UPDATE dbo.MembershipTypes SET name = 'Quarterly' WHERE Id = 3");
			Sql("UPDATE dbo.MembershipTypes SET name = 'Yearly' WHERE Id = 4");
		}
	}
}
