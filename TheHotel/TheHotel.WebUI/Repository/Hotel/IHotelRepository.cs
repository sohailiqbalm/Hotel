using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHotel.Common.Core;

namespace TheHotel.WebUI.Repository.Hotel
{
    public interface IHotelRepository : IRepository<TheHotel.Data.DomainEntities.Hotel>
    {
        
    }

    public interface IStudentRepository : IRepository<TheHotel.Data.DomainEntities.Student>
    {

    }
}
