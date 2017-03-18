using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelmesBootcamp.Models;
using HelmesBootcamp.Repositories;

namespace HelmesBootcamp.Controllers
{
    public class ServiceLaneController : Controller
    {
        GenericRepository<DbServiceLine> repository;

        public ServiceLaneController(GenericRepository<DbServiceLine> repository)
        {
            this.repository = repository;
        }

        private void SetViewBag(int id)
        {
            ViewBag.GarageId = id;
            var existingLines = repository.FindAllAsQueryable(i => i.GarageId == id && !i.Deleted).Select(i => i.Id).ToList();
            var possible = Enumerable.Range(1, 10).ToList();
            possible.RemoveAll(i => existingLines.Contains(i));
            ViewBag.Lines = possible;
        }

        public ActionResult Create(int id)
        {
            SetViewBag(id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int garageId, int Id, bool VansTrucks)
        {
            SetViewBag(garageId);

            var line = repository.Find(garageId, Id);
            if (line == null)
            {
                line = new DbServiceLine() { VansTrucks = VansTrucks, Id = Id, GarageId = garageId };
                repository.Insert(line);
            }
            else
            {
                line.Deleted = false;
                line.VansTrucks = VansTrucks;
                repository.Update(line);
            }
            
            return RedirectToAction(nameof(GarageController.Details), "Garage", new { id = garageId });
        }

        public ActionResult Delete(int id, int garageId)
        {
            var line = repository.Find(garageId, id);
            line.Deleted = true;
            repository.Update(line);
            return RedirectToAction(nameof(GarageController.Details), "Garage", new {id = garageId });
        }
    }
}