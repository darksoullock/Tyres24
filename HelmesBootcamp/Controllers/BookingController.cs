using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelmesBootcamp.Models;
using HelmesBootcamp.Models.DTO;
using HelmesBootcamp.Models.DTO.Assemblers;
using HelmesBootcamp.Repositories;

namespace HelmesBootcamp.Controllers
{
    public class BookingController : Controller
    {
        GenericRepository<DbBooking> bookingRepository;
        GenericRepository<DbGarage> garageRepository;

        public BookingController(GenericRepository<DbBooking> bookingRepository,
                                GenericRepository<DbGarage> garageRepository)
        {
            this.bookingRepository = bookingRepository;
            this.garageRepository = garageRepository;
        }

        public ActionResult Index()
        {
            var bookings = bookingRepository.FindAllAsQueryable().OrderByDescending(i => i.StartDateTime).ToList();
            return View(bookings.Select(i => new BookingDTO(i)).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.Garages = garageRepository.FindAllAsQueryable();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookingDTO booking)
        {
            if (ModelState.IsValid)
            {
                bookingRepository.Insert(BookingAssembler.Create(booking));
                return RedirectToAction("Index");
            }

            ViewBag.Garages = garageRepository.FindAllAsQueryable();
            return View(booking);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Garages = garageRepository.FindAllAsQueryable();
            return View(new BookingDTO(bookingRepository.FindById(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookingDTO booking)
        {
            if (ModelState.IsValid)
            {
                var entity = bookingRepository.FindById(booking.Id);
                entity.ChangedAt = DateTime.Now;
                entity.Edited = true;
                bookingRepository.Update(BookingAssembler.Update(entity, booking));
                return RedirectToAction("Index");
            }

            ViewBag.Garages = garageRepository.FindAllAsQueryable();
            return View(booking);
        }

        public ActionResult Details(int id)
        {
            return View(new BookingDTO(bookingRepository.FindById(id)));
        }

        public ActionResult Cancel(int id)
        {
            var booking = bookingRepository.FindById(id);
            booking.ChangedAt = DateTime.Now;
            booking.Cancelled = true;
            bookingRepository.Update(booking);
            return RedirectToAction(nameof(Index));
        }
    }
}