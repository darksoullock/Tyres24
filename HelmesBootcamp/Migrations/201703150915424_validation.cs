namespace HelmesBootcamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Booking", "GarageId", "dbo.Garage");
            DropIndex("dbo.Booking", new[] { "GarageId" });
            AlterColumn("dbo.Booking", "GarageId", c => c.Int(nullable: false));
            AlterColumn("dbo.Booking", "LicensePlate", c => c.String(nullable: false));
            AlterColumn("dbo.Booking", "Phone", c => c.String(nullable: false));
            CreateIndex("dbo.Booking", "GarageId");
            AddForeignKey("dbo.Booking", "GarageId", "dbo.Garage", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Booking", "GarageId", "dbo.Garage");
            DropIndex("dbo.Booking", new[] { "GarageId" });
            AlterColumn("dbo.Booking", "Phone", c => c.String());
            AlterColumn("dbo.Booking", "LicensePlate", c => c.String());
            AlterColumn("dbo.Booking", "GarageId", c => c.Int());
            CreateIndex("dbo.Booking", "GarageId");
            AddForeignKey("dbo.Booking", "GarageId", "dbo.Garage", "Id");
        }
    }
}
