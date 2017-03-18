namespace HelmesBootcamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class garagelinesoftdelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Garage", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ServiceLane", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceLane", "Deleted");
            DropColumn("dbo.Garage", "Deleted");
        }
    }
}
