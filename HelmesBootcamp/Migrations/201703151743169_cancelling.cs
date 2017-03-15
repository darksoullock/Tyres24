namespace HelmesBootcamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cancelling : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Booking", "Edited", c => c.Boolean(nullable: false));
            AddColumn("dbo.Booking", "Cancelled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Booking", "ChangedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Booking", "ChangedAt");
            DropColumn("dbo.Booking", "Cancelled");
            DropColumn("dbo.Booking", "Edited");
        }
    }
}
