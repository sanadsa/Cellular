namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTemplatePackagesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TemplatePackages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageName = c.String(),
                        TotalPrice = c.Double(nullable: false),
                        MaxMinute = c.Int(nullable: false),
                        MinutePrice = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        FavoriteNumber = c.Boolean(nullable: false),
                        MostCalledNumbers = c.Boolean(nullable: false),
                        FamilyDiscount = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TemplatePackages");
        }
    }
}
