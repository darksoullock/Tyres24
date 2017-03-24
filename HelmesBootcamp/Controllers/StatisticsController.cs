using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelmesBootcamp.Handlers;

namespace HelmesBootcamp.Controllers
{
    public class StatisticsController : Controller
    {
        GetMonthlyDynamics getMD;
        GetWeeklyBookings getWB;

        public StatisticsController(GetMonthlyDynamics getMD, GetWeeklyBookings getWB)
        {
            this.getMD = getMD;
            this.getWB = getWB;
        }

        public ActionResult MonthlyDynamics()
        {
            return View(getMD.Execute());
        }

        public ActionResult WeeklyBookings(DateTime? _week = null, int type = 3)
        {
            var week = _week.HasValue ? _week.Value : DateTime.Now;
            var weekStart = week.AddDays(1 - (int)week.DayOfWeek);
            ViewBag.Current = weekStart;
            ViewBag.Type = type;

            return View(getWB.Execute(week, type));
        }
    }
}