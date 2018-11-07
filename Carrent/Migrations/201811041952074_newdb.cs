namespace Carrent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Color = c.String(),
                        Made = c.String(),
                        Registration = c.String(),
                        ImageUrl = c.String(),
                        CarTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarTypes", t => t.CarTypeId, cascadeDelete: true)
                .Index(t => t.CarTypeId);
            
            CreateTable(
                "dbo.CarTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MonthlyRent = c.String(),
                        DailyRent = c.String(),
                        HourlyRent = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CarInfoId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarInfoes", t => t.CarInfoId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.CarInfoId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Rents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        CarInfoId = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarInfoes", t => t.CarInfoId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.CarInfoId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rents", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Rents", "CarInfoId", "dbo.CarInfoes");
            DropForeignKey("dbo.Owners", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Owners", "CarInfoId", "dbo.CarInfoes");
            DropForeignKey("dbo.CarInfoes", "CarTypeId", "dbo.CarTypes");
            DropIndex("dbo.Rents", new[] { "CarInfoId" });
            DropIndex("dbo.Rents", new[] { "CustomerId" });
            DropIndex("dbo.Owners", new[] { "User_Id" });
            DropIndex("dbo.Owners", new[] { "CarInfoId" });
            DropIndex("dbo.CarInfoes", new[] { "CarTypeId" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.Rents");
            DropTable("dbo.Owners");
            DropTable("dbo.Customers");
            DropTable("dbo.CarTypes");
            DropTable("dbo.CarInfoes");
        }
    }
}
