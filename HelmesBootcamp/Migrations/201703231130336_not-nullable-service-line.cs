namespace HelmesBootcamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notnullableserviceline : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Booking", new[] { "GarageId", "ServiceLineId" }, "dbo.ServiceLane");
            DropIndex("dbo.Booking", new[] { "GarageId", "ServiceLineId" });
            AlterColumn("dbo.Booking", "ServiceLineId", c => c.Int(nullable: false));
            CreateIndex("dbo.Booking", new[] { "GarageId", "ServiceLineId" });
            AddForeignKey("dbo.Booking", new[] { "GarageId", "ServiceLineId" }, "dbo.ServiceLane", new[] { "GarageId", "Id" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Booking", new[] { "GarageId", "ServiceLineId" }, "dbo.ServiceLane");
            DropIndex("dbo.Booking", new[] { "GarageId", "ServiceLineId" });
            AlterColumn("dbo.Booking", "ServiceLineId", c => c.Int());
            CreateIndex("dbo.Booking", new[] { "GarageId", "ServiceLineId" });
            AddForeignKey("dbo.Booking", new[] { "GarageId", "ServiceLineId" }, "dbo.ServiceLane", new[] { "GarageId", "Id" });
        }
    }
}
