using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using TheHotel.Common.Core;
using TheHotel.Common.Helpers;
using TheHotel.WebUI.Models;
using TheHotel.Common.Extensions;

namespace TheHotel.WebUI.Repository.Hotel
{
    public class HotelRepository : IHotelRepository
    {
        private readonly string serviceEndpoint;

        public HotelRepository(string serviceEndpoint)
        {
            this.serviceEndpoint = serviceEndpoint;
        }

        public TheHotel.Data.DomainEntities.Hotel Get(int id)
        {
            return HttpRequestHelper.Get<TheHotel.Data.DomainEntities.Hotel>(this.serviceEndpoint, "api/hotels/" + id);
        }

        public List<TheHotel.Data.DomainEntities.Hotel> GetAll()
        {
            return HttpRequestHelper.Get<List<TheHotel.Data.DomainEntities.Hotel>>(this.serviceEndpoint, "api/hotels/");
        }

        public int Add(TheHotel.Data.DomainEntities.Hotel entity)
        {
            HttpResponseMessage response = null;

            response = HttpRequestHelper.Post(this.serviceEndpoint, "api/hotels", entity);


            if (response.Headers != null && response.Headers.Location != null)
            {
                return response.Headers.Location.AbsoluteUri.ExtractIdFromUrl();
            }

            return default(int);
        }

        public int Update(TheHotel.Data.DomainEntities.Hotel entity)
        {
            HttpResponseMessage response = null;

            response = HttpRequestHelper.Put(this.serviceEndpoint, "api/hotels/" + entity.Id, entity);
            
            if (response.Headers != null && response.Headers.Location != null)
            {
                return response.Headers.Location.AbsoluteUri.ExtractIdFromUrl();
            }

            return default(int);
        }
    }
}