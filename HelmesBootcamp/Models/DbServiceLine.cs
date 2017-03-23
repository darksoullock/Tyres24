using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelmesBootcamp.Models
{
    [Table("ServiceLane")]
    public class DbServiceLine
    {
        [Key, Column(Order = 2)]
        public int Id { get; set; }

        [Required]
        public bool VansTrucks { get; set; }

        [Key, Column(Order = 1)]
        public int? GarageId { get; set; }

        public virtual DbGarage Garage { get; set; }

        public bool Deleted { get; set; }

        [InverseProperty(nameof(DbBooking.ServiceLine))]
        public virtual ICollection<DbBooking> Bookings { get; set; }
    }
}