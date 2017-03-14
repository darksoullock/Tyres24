using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using HelmesBootcamp.Enums;

namespace HelmesBootcamp.Models {
    [Table("Booking")]
    public class DbBooking {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int? GarageId { get; set; }

        public virtual DbGarage Garage { get; set; }

        public VehicleType Type{ get; set; }

        [Required]
        [Display(Name = "Booking start date and time")]
        public DateTime StartDateTime { get; set; }

        [Required]
        [Display(Name = "Booking end date and time")]
        public DateTime EndDateTime { get; set; }

        public string LicensePlate { get; set; }

        public string Phone { get; set; }

        public bool TyreHotel { get; set; }

        public string Comments { get; set; }
    }
}