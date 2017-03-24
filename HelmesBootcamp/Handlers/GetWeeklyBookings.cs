using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HelmesBootcamp.Models;
using HelmesBootcamp.Models.DTO;
using HelmesBootcamp.Repositories;

namespace HelmesBootcamp.Handlers
{
    public class GetWeeklyBookings
    {
        GenericRepository<DbBooking> bookingRepository;
        GenericRepository<DbGarage> garageRepository;

        public GetWeeklyBookings(GenericRepository<DbBooking> bookingRepository,
                        GenericRepository<DbGarage> garageRepository)
        {
            this.bookingRepository = bookingRepository;
            this.garageRepository = garageRepository;
        }

        public IEnumerable<WeeklyBookingsDTO> Execute(DateTime week, int type)
        {
            bool cars = (type & 1) > 0;
            bool vans = (type & 2) > 0;
            week = week.Date;
            var weekStart = week.AddDays(1 - (int)week.DayOfWeek);
            var weekEnd = weekStart.AddDays(7);
            var bookings = bookingRepository
                .FindAllAsQueryable(i =>
                    !i.Cancelled &&
                    (i.Type == Models.Enums.VehicleType.Car && cars || i.Type != Models.Enums.VehicleType.Car && vans) &&
                    i.StartDateTime > weekStart &&
                    i.StartDateTime < weekEnd)
                .GroupBy(i=>i.GarageId)
                .ToList()
                .Select(i=>new WeeklyBookingsDTO(garageRepository.FindById(i.Key).Name, i.AsEnumerable()));

            return bookings;
        }
    }
}