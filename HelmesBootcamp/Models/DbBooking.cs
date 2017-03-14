using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelmesBootcamp.Models {
    /// <summary>
    /// We use "Db" before car to indicat that this class is reperesantion of some table in database
    /// </summary>
    [Table("Booking")]
    public class DbBooking {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Booking information")]
        public string Information { get; set; }
        [Required]
        [Display(Name = "Booking start date and time")]
        public DateTime StartDateTime { get; set; }
    }
}