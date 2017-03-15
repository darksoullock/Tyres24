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
        
        public ActionResult Index()
        {
            return View(db.Bookings.OrderByDescending(i=>i.StartDateTime).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.Garages = db.Garages;
            return View();
        }


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

            ViewBag.Garages = db.Garages;
            return View(dbbooking);
        }

        public ActionResult Details(int id)
        {
            return View(db.Bookings.FirstOrDefault(i=>i.Id==id));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}