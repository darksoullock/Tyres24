using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HelmesBootcamp.Models{
    public class BookingContext : DbContext {
        public BookingContext() : base("DefaultConnection") {

        }

        public DbSet<DbBooking> Bookings { get; set; }

        public DbSet<DbGarage> Garages { get; set; }
    }
}