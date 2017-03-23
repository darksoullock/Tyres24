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
            ViewBag.Garages = garageRepository.FindAllAsQueryable();
            var bookings = bookingRepository.FindAllAsQueryable().OrderByDescending(i => i.StartDateTime).ToList();
            return View(bookings.Select(i => new BookingDTO(i)).ToList());
        }

        [HttpPost]
        public ActionResult Index(BookingFilterDTO filter)
        {
            ViewBag.Garages = garageRepository.FindAllAsQueryable();
            ViewBag.Van = filter.Van;
            ViewBag.Date = filter.Date;
            List<DbBooking> bookings = GetFilteredBookings(filter);
            return View(bookings.Select(i => new BookingDTO(i)).ToList());
        }

        // TODO move
        private List<DbBooking> GetFilteredBookings(BookingFilterDTO filter)
        {
            var bookings = bookingRepository.FindAllAsQueryable(i=>i.GarageId == filter.GarageId);
            if (filter.Van)
            {
                bookings = bookings.Where(i => i.Type != Models.Enums.VehicleType.Car);
            }
            else
            {
                bookings = bookings.Where(i => i.Type == Models.Enums.VehicleType.Car);
            }

            if (filter.Date!=null)
            {
                bookings = bookings.Where(i => i.StartDateTime.Date == filter.Date.Value.Date);
            }

            return bookings.OrderByDescending(i => i.StartDateTime).ToList();
        }

        public ActionResult Create()
        {
            ViewBag.Garages = garageRepository.FindAllAsQueryable();
            return View();
        }

        private void UpdateBooking(BookingDTO booking)
        {
            booking.EndDateTime = booking.StartDateTime.AddMinutes(booking.Type == Models.Enums.VehicleType.Truck ? 60 : 30);
            var garage = garageRepository.FindById(booking.GarageId);
            if (booking.Type != Models.Enums.VehicleType.Car)
            {
                if (!garage.ServiceLanes.Any(i => i.VansTrucks))
                    ModelState.AddModelError(nameof(BookingDTO.GarageId), "No vans/truck service lines available in this garage");
            }

            CheckAvailableSpace(booking, garage);

        }

        // TODO move somewhere
        private void CheckAvailableSpace(BookingDTO booking, DbGarage garage)
        {
            var intersectingBookings = bookingRepository.FindAllAsQueryable(i =>
                                            i.GarageId == booking.GarageId &&
                                            i.StartDateTime < booking.EndDateTime &&
                                            i.EndDateTime > booking.StartDateTime);

            bool check;
            int lines = garage.ServiceLanes.Count(i => !i.Deleted);
            check = intersectingBookings.Count() < lines; // free lines available

            if (check && booking.Type != Models.Enums.VehicleType.Car)
            {
                int truckLines = garage.ServiceLanes.Count(i => !i.Deleted && i.VansTrucks);
                check = intersectingBookings.Count(i => i.Type != Models.Enums.VehicleType.Car) < truckLines; // free van/truck lines available
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookingDTO booking)
        {
            UpdateBooking(booking);

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
            ViewBag.Garages = garageRepository.FindAllAsQueryable(i => !i.Deleted);
            return View(new BookingDTO(bookingRepository.FindById(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookingDTO booking)
        {
            UpdateBooking(booking);

            if (ModelState.IsValid)
            {
                var entity = bookingRepository.FindById(booking.Id);
                entity.ChangedAt = DateTime.Now;
                entity.Edited = true;
                bookingRepository.Update(BookingAssembler.Update(entity, booking));
                return RedirectToAction("Index");
            }

            ViewBag.Garages = garageRepository.FindAllAsQueryable(i=>!i.Deleted);
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