using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelmesBootcamp.Models.DTO
{
    public class MonthlyDynamicsDTO
    {
        public string Garage { get; set; }
        public int ServiceLine { get; set; }
        public int PreviousMonth { get; set; }
        public int CurrentMonth { get; set; }
        public string Percentage => PreviousMonth == 0 ? "-" : (CurrentMonth*100 / PreviousMonth).ToString();
    }
}