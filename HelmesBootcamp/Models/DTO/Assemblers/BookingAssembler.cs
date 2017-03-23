using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelmesBootcamp.Models.DTO.Assemblers
{
    public class BookingAssembler
    {
        public static DbBooking Create(BookingDTO dto)
        {
            return Update(new DbBooking(), dto);
        }

        public static DbBooking Update(DbBooking booking, BookingDTO dto)
        {
            booking.Comments = dto.Comments;
            booking.EndDateTime = dto.EndDateTime;
            booking.GarageId = dto.GarageId;
            booking.ServiceLineId = dto.ServiceLineId;
            booking.Id = dto.Id;
            booking.LicensePlate = dto.LicensePlate;
            booking.Phone = dto.Phone;
            booking.StartDateTime = dto.StartDateTime;
            booking.Type = dto.Type;
            booking.TyreHotel = dto.TyreHotel;
            return booking;
        }
    }
}