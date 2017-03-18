namespace HelmesBootcamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class servicelane : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceLane",
                c => new
                    {
                        GarageId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        VansTrucks = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.GarageId, t.Id })
                .ForeignKey("dbo.Garage", t => t.GarageId, cascadeDelete: true)
                .Index(t => t.GarageId);
            
            AlterColumn("dbo.Garage", "Name", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Garage", "Address", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceLane", "GarageId", "dbo.Garage");
            DropIndex("dbo.ServiceLane", new[] { "GarageId" });
            AlterColumn("dbo.Garage", "Address", c => c.String());
            AlterColumn("dbo.Garage", "Name", c => c.String());
            DropTable("dbo.ServiceLane");
        }
    }
}
