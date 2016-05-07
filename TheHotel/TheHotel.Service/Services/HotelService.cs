using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHotel.Data.DomainEntities;
using TheHotel.Infrastructure.Repository;

namespace TheHotel.Service.Services
{
    public class HotelService : IHotelService
    {
        IHotelRepository hotelRepository;

        public HotelService(IHotelRepository hotelRepository) 
        {
            this.hotelRepository = hotelRepository;
        }

        public Hotel GetHotel(int id) 
        {
            return hotelRepository.Get(id);
        }

        public List<Hotel> GetHotels()
        {
            return hotelRepository.GetAll();
        }

        public int AddHotel(Hotel hotel) 
        {
            if (this.hotelRepository.GetHotelByName(hotel.Name) != null)
            {
                throw new Exception(string.Format("The hotel name '{0}' already exists.", hotel.Name));
            }

            // Add the colour to database
            return this.hotelRepository.Add(hotel);
        }
        public void UpdateHotel(Hotel hotel)
        {
            if (this.hotelRepository.Get(hotel.Id) == null)
            {
                throw new Exception(string.Format("The hotel with id '{0}' does not exist.", hotel.Id));
            }

            // Check that a colour with this code doesn't already exist
            Hotel existingHotel = this.hotelRepository.GetHotelByName(hotel.Name);
            if (existingHotel != null && existingHotel.Id != hotel.Id)
            {
                throw new  Exception(string.Format("The hotel name '{0}' already exists.", hotel.Name));
            }

            // Update the colour in the database
            this.hotelRepository.Update(hotel);
        }
    }
}
