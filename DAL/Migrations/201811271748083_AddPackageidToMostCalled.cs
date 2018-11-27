namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPackageidToMostCalled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MostCalleds", "PackageId", c => c.Int(nullable: false));
            CreateIndex("dbo.MostCalleds", "PackageId");
            AddForeignKey("dbo.MostCalleds", "PackageId", "dbo.Packages", "PackageId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MostCalleds", "PackageId", "dbo.Packages");
            DropIndex("dbo.MostCalleds", new[] { "PackageId" });
            DropColumn("dbo.MostCalleds", "PackageId");
        }
    }
}
