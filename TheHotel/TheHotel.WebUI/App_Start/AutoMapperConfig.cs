using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheHotel.Data.DomainEntities;
using TheHotel.WebUI.Models;

namespace TheHotel.WebUI.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Hotel, HotelModel>();
            Mapper.CreateMap<HotelModel, Hotel>();
        }
    }
}