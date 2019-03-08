namespace Vidly.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class SeedUsers : DbMigration
	{
		public override void Up()
		{
			Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [DrivingLicense], [Phone], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2e25734c-f292-4434-bee0-c015a964cc32', N'foo', N'01234 567890', N'guest@vidly.com', 0, N'AEGOW7uuRPcf+B5JGjXCLAI/zFFyYWvquFiXLEib+gYWyzRl93br89G5+l6/tUSNEg==', N'51586599-fee0-4064-b28c-70185d5a2ef3', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')");
			Sql("INSERT INTO[dbo].[AspNetUsers]([Id], [DrivingLicense], [Phone], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'7a60a2e9-4dcb-497e-acfc-b832256280ad', N'foo', N'01234 567890', N'admin@vidly.com', 0, N'AC6fBJAtjn2giR5QaMwEINaW9DGJRy2BO7MQ/cD/m6GtroGk776Wg9k91AFi1tTBOA==', N'4f8c4e29-4dac-4039-a8c2-0337863323d7', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')");
			Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'746ff64f-4cf8-410d-b04c-65ba0efafadd', N'CanManageMovies')");
			Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7a60a2e9-4dcb-497e-acfc-b832256280ad', N'746ff64f-4cf8-410d-b04c-65ba0efafadd')");
		}
		
		public override void Down()
		{
		}
	}
}
