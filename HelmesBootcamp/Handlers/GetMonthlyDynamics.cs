using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HelmesBootcamp.Models;
using HelmesBootcamp.Models.DTO;
using HelmesBootcamp.Repositories;

namespace HelmesBootcamp.Handlers
{
    public class GetMonthlyDynamics
    {
        GenericRepository<DbBooking> bookingRepository;

        public GetMonthlyDynamics(GenericRepository<DbBooking> bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        public List<MonthlyDynamicsDTO> Execute()
        {
            DateTime now = DateTime.Now;
            DateTime month = new DateTime(now.Year, now.Month, 1);
            DateTime previousMonth = month.AddMonths(-1);
            var bookings = bookingRepository.FindAllAsQueryable(i => i.StartDateTime >= previousMonth && !i.Cancelled);
            var r = bookings.GroupBy(i=>new { i.GarageId, i.ServiceLine}).Select(i=>
                new MonthlyDynamicsDTO(){
                    Garage = i.Key.ServiceLine.Garage.Name,
                    ServiceLine = i.Key.ServiceLine.Id,
                    PreviousMonth = i.Count(j => j.EndDateTime < month),
                    CurrentMonth = i.Count(j => j.EndDateTime >= month)
                }).ToList();
            return r;
        }
    }
}