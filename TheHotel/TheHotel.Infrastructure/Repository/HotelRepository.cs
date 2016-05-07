using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHotel.Data.DbFactory;
using TheHotel.Data.DomainEntities;

namespace TheHotel.Infrastructure.Repository
{
    public class HotelRepository : IHotelRepository
    {
        public Hotel Get(int id) 
        { 
            using (var context = new HotelContext())
            {
                return context.Hotels.FirstOrDefault(x=>x.Id == id);
            }
        }

        public List<Hotel> GetAll()
        {
            using (var context = new HotelContext())
            {
                return context.Hotels.ToList();
            }
        }

        public Hotel GetHotelByName(string hotelName) 
        {
            using (var context = new HotelContext())
            {
                return context.Hotels.FirstOrDefault(x => x.Name == hotelName);
            }
        }

        public int Add(Hotel hotel)
        {
            using (var context = new HotelContext())
            {
                // Add the hotel to database
                context.Hotels.Add(hotel);
                context.SaveChanges();
            }

            return hotel.Id;
        }

        public int Update(Hotel hotel)
        {
            using (var context = new HotelContext())
            {
                // Get the existing hotel
                Hotel existingHotel = context.Hotels.Where(x => x.Id == hotel.Id).FirstOrDefault();

                if (existingHotel == null)
                {
                    throw new Exception(string.Format("The hotel with id '{0}' does not exist.", hotel.Id));
                }

                // Update the values
                DbEntityEntry entry = context.Entry(existingHotel);
                entry.CurrentValues.SetValues(hotel);

                context.SaveChanges();

                return hotel.Id;
            }
        }

        //List<T> GetAll();
        //void Add(T entity);
        //void Update(T entity);
        //void Delete(int id);

    }
}
