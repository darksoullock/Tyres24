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
            var bookings = bookingRepository.FindAllAsQueryable(i => i.GarageId == filter.GarageId);
            if (filter.Van)
            {
                bookings = bookings.Where(i => i.Type != Models.Enums.VehicleType.Car);
            }
            else
            {
                bookings = bookings.Where(i => i.Type == Models.Enums.VehicleType.Car);
            }

            if (filter.Date != null)
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
        }

        // TODO move somewhere
        private int? FindAvailableSpace(BookingDTO booking)
        {
            var intersectingBookings = bookingRepository.FindAllAsQueryable(i =>
                                            i.GarageId == booking.GarageId &&
                                            i.StartDateTime < booking.EndDateTime &&
                                            i.EndDateTime > booking.StartDateTime &&
                                            i.Id != booking.Id);

            var takenSLs = intersectingBookings.Select(j => j.ServiceLineId).ToList();
            var freeSLs = garageRepository.FindById(booking.GarageId).ServiceLanes.Where(i => !i.Deleted && !takenSLs.Contains(i.Id)).ToList();
            if (booking.Type != Models.Enums.VehicleType.Car)
            {
                freeSLs = freeSLs.Where(i => i.VansTrucks).ToList();
            }

            if (freeSLs.Count == 0)
            {
                return null;
            }
            else
            {
                return freeSLs.OrderBy(i => i.VansTrucks).First().Id;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookingDTO booking)
        {
            UpdateBooking(booking);
            var line = FindAvailableSpace(booking);
            if (line == null)
                ModelState.AddModelError(nameof(BookingDTO.GarageId), "No free service lines available in this garage at this time");
            else
                booking.ServiceLineId = line.Value;

            int slots = !booking.TyreHotel ? 0 : booking.Type == Models.Enums.VehicleType.Car ? 1 : 2;
            var garage = garageRepository.FindById(booking.GarageId);
            if (garage.TyreSlots < slots)
            {
                ModelState.AddModelError(nameof(BookingDTO.TyreHotel), "Not enough free slots");
            }
            else
            {
                garage.TyreSlots -= slots;
                garageRepository.Update(garage);
            }

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
            // if car type or hotel changed or check for tyre slots here
            UpdateBooking(booking);
            var line = FindAvailableSpace(booking);
            if (line == null)
                ModelState.AddModelError(nameof(BookingDTO.GarageId), "No free service lines available in this garage at this time");
            else
                booking.ServiceLineId = line.Value;

            if (ModelState.IsValid)
            {
                var entity = bookingRepository.FindById(booking.Id);
                entity.ChangedAt = DateTime.Now;
                entity.Edited = true;
                bookingRepository.Update(BookingAssembler.Update(entity, booking));
                return RedirectToAction("Index");
            }

            ViewBag.Garages = garageRepository.FindAllAsQueryable(i => !i.Deleted);
            return View(booking);
        }

        public ActionResult Details(int id)
        {
            return View(new BookingDTO(bookingRepository.FindById(id)));
        }

        public ActionResult Cancel(int id)
        {
            var booking = bookingRepository.FindById(id);
            int slots = !booking.TyreHotel ? 0 : booking.Type == Models.Enums.VehicleType.Car ? 1 : 2;
            if (slots > 0)
            {
                var garage = garageRepository.FindById(booking.GarageId);
                garage.TyreSlots += slots;
                garageRepository.Update(garage);
            }
            
            booking.ChangedAt = DateTime.Now;
            booking.Cancelled = true;
            bookingRepository.Update(booking);
            return RedirectToAction(nameof(Index));
        }
    }
}