using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using HelmesBootcamp.Models.Enums;
using HelmesBootcamp.Models.Validators;

namespace HelmesBootcamp.Models {
    [Table("Booking")]
    public class DbBooking {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int? GarageId { get; set; }

        public virtual DbGarage Garage { get; set; }

        [Required]
        public VehicleType Type{ get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        [Required]
        public string LicensePlate { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public bool TyreHotel { get; set; }

        public string Comments { get; set; }

        public bool Edited { get; set; }

        public bool Cancelled { get; set; }

        public DateTime? ChangedAt { get; set; }
    }
}