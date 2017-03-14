namespace HelmesBootcamp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<HelmesBootcamp.Models.BookingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "HelmesBootcamp.Models.BookingContext";
        }

        protected override void Seed(HelmesBootcamp.Models.BookingContext context)
        {
            context.Garages.AddOrUpdate(i => i.Name,
                new DbGarage() { Name = "Nõmme", TyreHotel = true },
                new DbGarage() { Name = "Lasnamäe", TyreHotel = false },
                new DbGarage() { Name = "Mustamäe", TyreHotel = true });

            //  This method will be called after migrating to the latest version.
        }
    }
}
