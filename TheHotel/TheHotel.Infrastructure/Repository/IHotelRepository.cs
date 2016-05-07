using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHotel.Common.Core;
using TheHotel.Data.DomainEntities;

namespace TheHotel.Infrastructure.Repository
{
    public interface IHotelRepository: IRepository<Hotel>
    {
        Hotel GetHotelByName(string hotelName);
    }
}
