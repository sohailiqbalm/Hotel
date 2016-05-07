using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHotel.Data.DomainEntities;

namespace TheHotel.Service.Services
{
    public interface IHotelService
    {
        Hotel GetHotel(int id);
        List<Hotel> GetHotels();
        int AddHotel(Hotel hotel);
        void UpdateHotel(Hotel hotel);
    }
}
