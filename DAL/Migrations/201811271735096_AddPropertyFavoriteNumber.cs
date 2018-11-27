namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertyFavoriteNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "FavoriteNumber", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "FavoriteNumber");
        }
    }
}
