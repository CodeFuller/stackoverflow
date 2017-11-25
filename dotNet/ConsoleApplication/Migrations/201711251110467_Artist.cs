namespace ConsoleApplication.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class Artist : DbMigration
	{
		public override void Up()
		{
			CreateTable(
					"dbo.Albums",
					c => new
					{
						Id = c.Guid(nullable: false),
						Title = c.String(),
						Artist_Id = c.Guid(),
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Artists", t => t.Artist_Id)
				.Index(t => t.Artist_Id);

			CreateTable(
					"dbo.Artists",
					c => new
					{
						Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newid()"),
						Name = c.String(),
					})
				.PrimaryKey(t => t.Id);
		}

		public override void Down()
		{
			DropForeignKey("dbo.Albums", "Artist_Id", "dbo.Artists");
			DropIndex("dbo.Albums", new[] { "Artist_Id" });
			DropTable("dbo.Artists");
			DropTable("dbo.Albums");
		}
	}
}
