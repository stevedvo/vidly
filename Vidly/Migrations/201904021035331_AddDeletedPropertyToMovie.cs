namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeletedPropertyToMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Deleted");
        }
    }
}
