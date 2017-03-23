using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelmesBootcamp.Models.DTO
{
    public class BookingFilterDTO
    {
        public int GarageId { get; set; }
        public DateTime? Date { get; set; }
        public bool Van { get; set; }
    }
}