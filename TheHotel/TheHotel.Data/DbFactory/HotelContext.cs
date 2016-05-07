using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHotel.Data.DomainEntities;

namespace TheHotel.Data.DbFactory
{
    public class HotelContext : DbContext
    {
        public HotelContext() : base("TheHotelConnection")
        { }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
