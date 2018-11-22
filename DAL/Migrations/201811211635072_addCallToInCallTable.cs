namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCallToInCallTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Calls", "CallTo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Calls", "CallTo");
        }
    }
}
