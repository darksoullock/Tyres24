using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelmesBootcamp.Models
{
    public class DbWeatherInfo
    {
        public DbWeatherInfo(){}

        public DbWeatherInfo(DateTime date)
        {
            Date = date.Date;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public bool Snow { get; set; }
    }
}