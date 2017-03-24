using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelmesBootcamp.Models.DTO
{
    public class WeeklyBookingsDTO
    {
        public WeeklyBookingsDTO()
        {
            Days = new Dictionary<DayOfWeek, int>();
        }

        public WeeklyBookingsDTO(string garage, IEnumerable<DbBooking> bookings)
            :this()
        {
            Garage = garage;
            foreach (var i in bookings.GroupBy(i => i.StartDateTime.DayOfWeek))
            {
                Days.Add(i.Key, i.Count());
            }
        }

        public string Garage { get; set; }
        public Dictionary<DayOfWeek, int> Days { get; set; }
    }
}