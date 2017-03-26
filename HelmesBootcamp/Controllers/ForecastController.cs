using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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

        private void UpdateForecast()
        {
            string url = "http://www.ilmateenistus.ee/ilma_andmed/xml/forecast.php?lang=eng";
            var wc = new WebClient();
            var xml = wc.DownloadString(url);

            var days = weatherRepository.FindAllAsQueryable(i => i.Date > DateTime.Now).ToList();

            // probably not the best way.. probably can be replaced with xml deserialisation or linqToXml or something...
            var day1rgx = new Regex(@"Harku[\s\S]+?phenomenon>(.*?)<");
            var day1 = day1rgx.Matches(xml);
            days[0].Snow = day1[0].Groups[1].Value.Contains("snow");

            var day14rgx = new Regex(@"<day>[\s\S]+?phenomenon>(.*?)<");
            var day14 = day14rgx.Matches(xml);
            days[1].Snow = day14[1].Groups[1].Value.Contains("snow");
            days[2].Snow = day14[2].Groups[1].Value.Contains("snow");
            days[3].Snow = day14[3].Groups[1].Value.Contains("snow");
        }

        public ActionResult Index()
        {
            bool b = AddNewDays();
            if (b)
                UpdateForecast();
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