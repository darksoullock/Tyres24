using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelmesBootcamp.Models
{
    [Table("Garage")]
    public class DbGarage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Name cannot be longer than 15 characters.")]
        public string Name { get; set; }

        [Required]
        public bool TyreHotel { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        public bool Deleted { get; set; }

        [InverseProperty(nameof(DbServiceLine.Garage))]
        public virtual ICollection<DbServiceLine> ServiceLanes { get; set; }
    }
}