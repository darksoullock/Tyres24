using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelmesBootcamp.Models;

namespace HelmesBootcamp.Controllers
{
    public class BookingController : Controller
    {
        private BookingContext db = new BookingContext();

        //
        // GET: /Booking/

        public ActionResult Index()
        {
            return View(db.Bookings.ToList());
        }

        //
        // GET: /Booking/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Booking/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DbBooking dbbooking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(dbbooking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dbbooking);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}