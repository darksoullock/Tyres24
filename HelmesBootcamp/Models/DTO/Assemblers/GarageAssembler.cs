using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelmesBootcamp.Models.DTO.Assemblers
{
    public class GarageAssembler
    {
        public static DbGarage Create(GarageDTO dto)
        {
            return Update(new DbGarage(), dto);
        }

        public static DbGarage Update(DbGarage garage, GarageDTO dto)
        {
            garage.Id = dto.Id;
            garage.Name = dto.Name;
            garage.Address = dto.Address;
            garage.TyreHotel = dto.TyreHotel;
            garage.TyreSlots = dto.TyreSlots;
            return garage;
        }
    }
}