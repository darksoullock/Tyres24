using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelmesBootcamp.Models;
using HelmesBootcamp.Repositories;

namespace HelmesBootcamp.Controllers
{
    public class ForecastController : Controller
    {
        const int ndays = 4;
        GenericRepository<DbWeatherInfo> weatherRepository;

        public ForecastController(GenericRepository<DbWeatherInfo> weatherRepository)
        {
            this.weatherRepository = weatherRepository;
        }

        private bool AddNewDays()
        {
            bool b = false;
            var now = DateTime.Now;
            int cnt = weatherRepository.Count(i => i.Date > now);
            if (cnt<ndays)
            {
                var days = weatherRepository.FindAllAsQueryable(i => i.Date > now).ToList();
                days = Enumerable.Range(1, ndays).Select(i => new DbWeatherInfo(now.AddDays(i))).Where(i=>!days.Any(j=>j.Date==i.Date)).ToList();
                weatherRepository.Insert(days);
                b = true;
            }

            return b;
        }

        public ActionResult Index()
        {
            AddNewDays();
            return View(weatherRepository.FindAllAsQueryable(i=>i.Date>DateTime.Now));
        }

        public ActionResult Toggle(int id)
        {
            var e = weatherRepository.FindById(id);
            e.Snow = !e.Snow;
            weatherRepository.Update(e);
            return RedirectToAction(nameof(Index));
        }
    }
}