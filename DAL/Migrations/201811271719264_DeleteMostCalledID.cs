namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteMostCalledID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Packages", "MostCalled_MostCalledId", "dbo.MostCalleds");
            DropIndex("dbo.Packages", new[] { "MostCalled_MostCalledId" });
            DropColumn("dbo.Packages", "FavoriteNumId");
            DropColumn("dbo.Packages", "MostCalled_MostCalledId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "MostCalled_MostCalledId", c => c.Int());
            AddColumn("dbo.Packages", "FavoriteNumId", c => c.Int(nullable: false));
            CreateIndex("dbo.Packages", "MostCalled_MostCalledId");
            AddForeignKey("dbo.Packages", "MostCalled_MostCalledId", "dbo.MostCalleds", "MostCalledId");
        }
    }
}
