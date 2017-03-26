namespace HelmesBootcamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tyreslots : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Garage", "TyreSlots", c => c.Int());
            Sql("UPDATE dbo.Garage SET TyreSlots = 200");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Garage", "TyreSlots");
        }
    }
}
