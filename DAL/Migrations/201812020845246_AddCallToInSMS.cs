namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCallToInSMS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SMS", "CallTo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SMS", "CallTo");
        }
    }
}
