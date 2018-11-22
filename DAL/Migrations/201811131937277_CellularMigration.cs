namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CellularMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Calls",
                c => new
                    {
                        CallId = c.Int(nullable: false, identity: true),
                        LineID = c.Int(nullable: false),
                        Month = c.DateTime(nullable: false),
                        DestinationNum = c.String(),
                        Duration = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CallId)
                .ForeignKey("dbo.Lines", t => t.LineID, cascadeDelete: true)
                .Index(t => t.LineID);
            
            CreateTable(
                "dbo.Lines",
                c => new
                    {
                        LineId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Number = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LineId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        ClientName = c.String(),
                        LastName = c.String(),
                        IdNumber = c.Int(nullable: false),
                        ClientTypeId = c.Int(nullable: false),
                        Address = c.String(),
                        ContactNumber = c.String(),
                        CallsToCenter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClientID)
                .ForeignKey("dbo.ClientTypes", t => t.ClientTypeId, cascadeDelete: true)
                .Index(t => t.ClientTypeId);
            
            CreateTable(
                "dbo.ClientTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                        MinutePrice = c.Double(nullable: false),
                        SmsPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MostCalleds",
                c => new
                    {
                        MostCalledId = c.Int(nullable: false, identity: true),
                        FirstNumber = c.String(),
                        SecondNumber = c.String(),
                        ThirdNumber = c.String(),
                    })
                .PrimaryKey(t => t.MostCalledId);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        PackageId = c.Int(nullable: false, identity: true),
                        PackageName = c.String(),
                        LineId = c.Int(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        MaxMinute = c.Int(nullable: true),
                        DiscountPercentage = c.Double(nullable: true),
                        FavoriteNumId = c.Int(nullable: true),
                        MostCalledNums = c.Boolean(nullable: true),
                        FamilyDiscount = c.Boolean(nullable: true),
                        MinutePrice = c.Double(nullable: true),
                        Month = c.DateTime(nullable: false),
                        MostCalled_MostCalledId = c.Int(),
                    })
                .PrimaryKey(t => t.PackageId)
                .ForeignKey("dbo.Lines", t => t.LineId, cascadeDelete: true)
                .ForeignKey("dbo.MostCalleds", t => t.MostCalled_MostCalledId)
                .Index(t => t.LineId)
                .Index(t => t.MostCalled_MostCalledId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(nullable: false),
                        Month = c.DateTime(nullable: false),
                        TotalPayment = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .Index(t => t.ClientID);
            
            CreateTable(
                "dbo.ServiceAgents",
                c => new
                    {
                        ServiceAgentId = c.Int(nullable: false, identity: true),
                        Password = c.String(),
                        SalesAmount = c.Int(nullable: false),
                        AgentName = c.String(),
                    })
                .PrimaryKey(t => t.ServiceAgentId);
            
            CreateTable(
                "dbo.SMS",
                c => new
                    {
                        SmsID = c.Int(nullable: false, identity: true),
                        LineID = c.Int(nullable: false),
                        Month = c.DateTime(nullable: false),
                        DestinationNum = c.String(),
                    })
                .PrimaryKey(t => t.SmsID)
                .ForeignKey("dbo.Lines", t => t.LineID, cascadeDelete: true)
                .Index(t => t.LineID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SMS", "LineID", "dbo.Lines");
            DropForeignKey("dbo.Payments", "ClientID", "dbo.Clients");
            DropForeignKey("dbo.Packages", "MostCalled_MostCalledId", "dbo.MostCalleds");
            DropForeignKey("dbo.Packages", "LineId", "dbo.Lines");
            DropForeignKey("dbo.Calls", "LineID", "dbo.Lines");
            DropForeignKey("dbo.Lines", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "ClientTypeId", "dbo.ClientTypes");
            DropIndex("dbo.SMS", new[] { "LineID" });
            DropIndex("dbo.Payments", new[] { "ClientID" });
            DropIndex("dbo.Packages", new[] { "MostCalled_MostCalledId" });
            DropIndex("dbo.Packages", new[] { "LineId" });
            DropIndex("dbo.Clients", new[] { "ClientTypeId" });
            DropIndex("dbo.Lines", new[] { "ClientId" });
            DropIndex("dbo.Calls", new[] { "LineID" });
            DropTable("dbo.SMS");
            DropTable("dbo.ServiceAgents");
            DropTable("dbo.Payments");
            DropTable("dbo.Packages");
            DropTable("dbo.MostCalleds");
            DropTable("dbo.ClientTypes");
            DropTable("dbo.Clients");
            DropTable("dbo.Lines");
            DropTable("dbo.Calls");
        }
    }
}
