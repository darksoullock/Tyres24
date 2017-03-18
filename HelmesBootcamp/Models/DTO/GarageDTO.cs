using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelmesBootcamp.Models.DTO
{
    public class GarageDTO
    {
        public GarageDTO() { }

        public GarageDTO(DbGarage garage)
        {
            foreach (var i in typeof(DbGarage).GetProperties())
            {
                var property = typeof(GarageDTO).GetProperty(i.Name);
                if (property != null)
                {
                    property.SetValue(this, i.GetValue(garage));
                }
            }

            this.ServiceLanesList = garage.ServiceLanes?.Where(i => !i.Deleted)?.ToList() ?? new List<DbServiceLine>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        public bool TyreHotel { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        public List<DbServiceLine> ServiceLanesList { get; set; }
    }
}