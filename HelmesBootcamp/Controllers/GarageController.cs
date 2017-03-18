using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelmesBootcamp.Models;
using HelmesBootcamp.Models.DTO;
using HelmesBootcamp.Models.DTO.Assemblers;
using HelmesBootcamp.Repositories;

namespace HelmesBootcamp.Controllers
{
    public class GarageController : Controller
    {
        GenericRepository<DbGarage> garageRepository;

        public GarageController(GenericRepository<DbGarage> garageRepository)
        {
            this.garageRepository = garageRepository;
        }

        public ActionResult Index()
        {
            var garages = garageRepository.FindAllAsQueryable().OrderByDescending(i=>i.Id).ToList().Select(i => new GarageDTO(i)).ToList();
            return View(garages);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GarageDTO garage)
        {
            if (ModelState.IsValid)
            {
                garageRepository.Insert(GarageAssembler.Create(garage));
                return RedirectToAction("Index");
            }

            return View(garage);
        }

        public ActionResult Details(int id)
        {
            return View(new GarageDTO(garageRepository.FindById(id)));
        }

        public ActionResult Edit(int id)
        {
            return View(new GarageDTO(garageRepository.FindById(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GarageDTO garageDTO)
        {
            if (ModelState.IsValid)
            {
                var garage = garageRepository.FindById(garageDTO.Id);
                garageRepository.Update(GarageAssembler.Update(garage, garageDTO));
                return RedirectToAction("Index");
            }

            return View(garageDTO);
        }

        public ActionResult Delete(int id)
        {
            var garage = garageRepository.FindById(id);
            garageRepository.Remove(garage);
            return RedirectToAction(nameof(Index));
        }
    }
}