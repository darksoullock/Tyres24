namespace HelmesBootcamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creategarages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Garage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TyreHotel = c.Boolean(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Booking", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Booking", "EndDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Booking", "LicensePlate", c => c.String());
            AddColumn("dbo.Booking", "Phone", c => c.String());
            AddColumn("dbo.Booking", "TyreHotel", c => c.Boolean(nullable: false));
            AddColumn("dbo.Booking", "Comments", c => c.String());
            AddColumn("dbo.Booking", "GarageId", c => c.Int());
            CreateIndex("dbo.Booking", "GarageId");
            AddForeignKey("dbo.Booking", "GarageId", "dbo.Garage", "Id");
            DropColumn("dbo.Booking", "Information");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Booking", "Information", c => c.String(nullable: false));
            DropForeignKey("dbo.Booking", "GarageId", "dbo.Garage");
            DropIndex("dbo.Booking", new[] { "GarageId" });
            DropColumn("dbo.Booking", "GarageId");
            DropColumn("dbo.Booking", "Comments");
            DropColumn("dbo.Booking", "TyreHotel");
            DropColumn("dbo.Booking", "Phone");
            DropColumn("dbo.Booking", "LicensePlate");
            DropColumn("dbo.Booking", "EndDateTime");
            DropColumn("dbo.Booking", "Type");
            DropTable("dbo.Garage");
        }
    }
}
