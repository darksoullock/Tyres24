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
        [ValidVehicle]
        public VehicleType Type{ get; set; }

        [Required]
        [NoPastDate]
        [Display(Name = "Booking start date and time")]
        public DateTime StartDateTime { get; set; }

        [Required]
        [NoPastDate]
        [MaxDuration(2)]
        [Display(Name = "Booking end date and time")]
        public DateTime EndDateTime { get; set; }

        [Required]
        [RegularExpression("[A-Z0-9]{4,}", ErrorMessage = "Must have 4-9 characters and can only contain numbers (0...9) and Latin letters (A...Z)")] 
        public string LicensePlate { get; set; }

        [Required]
        [RegularExpression(@"\d{5,}", ErrorMessage = "Must have 4-9 characters and can only contain numbers (0...9) and Latin letters (A...Z)")]
        public string Phone { get; set; }

        public bool TyreHotel { get; set; }

        public string Comments { get; set; }
    }
}