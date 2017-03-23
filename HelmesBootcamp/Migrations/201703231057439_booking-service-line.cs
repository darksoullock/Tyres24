namespace HelmesBootcamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookingserviceline : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Booking", "GarageId", "dbo.Garage");
            DropIndex("dbo.Booking", new[] { "GarageId" });
            AddColumn("dbo.Booking", "ServiceLineId", c => c.Int());
            CreateIndex("dbo.Booking", new[] { "GarageId", "ServiceLineId" });
            AddForeignKey("dbo.Booking", new[] { "GarageId", "ServiceLineId" }, "dbo.ServiceLane", new[] { "GarageId", "Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Booking", new[] { "GarageId", "ServiceLineId" }, "dbo.ServiceLane");
            DropIndex("dbo.Booking", new[] { "GarageId", "ServiceLineId" });
            DropColumn("dbo.Booking", "ServiceLineId");
            CreateIndex("dbo.Booking", "GarageId");
            AddForeignKey("dbo.Booking", "GarageId", "dbo.Garage", "Id", cascadeDelete: true);
        }
    }
}
