namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlackFlagToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "BlackFlag", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "BlackFlag");
        }
    }
}
