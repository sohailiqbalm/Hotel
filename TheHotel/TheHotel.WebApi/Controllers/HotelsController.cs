using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheHotel.Data.DomainEntities;
using TheHotel.Service.Services;

namespace TheHotel.WebApi.Controllers
{
    public class HotelsController : ApiController
    {
        private IHotelService hotelService;

        public HotelsController(IHotelService hotelService) 
        {
            this.hotelService = hotelService;
        }

        public Hotel Get(int id) 
        {
            Hotel hotel = hotelService.GetHotel(id);

            if (hotel == null) 
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return hotel;
        }

        public List<Hotel> Get()
        {
            List<Hotel> hotels = hotelService.GetHotels();

            if (hotels == null || hotels.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return hotels;
        }

        public HttpResponseMessage Post(Hotel hotel)
        {
            HttpResponseMessage response = null;

            if (hotel == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            // Add the hotel to database
            int hotelId = this.hotelService.AddHotel(hotel);

            // Success
            response = new HttpResponseMessage(HttpStatusCode.Created);
            response.Headers.Location = new Uri(this.Request.RequestUri.AbsoluteUri + "/" + hotelId);

            return response;
        }

        public HttpResponseMessage Put(int id, Hotel hotel)
        {
            HttpResponseMessage response = null;

            if (hotel == null)
            {
                throw new Exception("No hotel information specified on the update request.");
            }

            // Update the hotel
            hotel.Id = id;
            this.hotelService.UpdateHotel(hotel);

            // Success
            response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.Location = new Uri(this.Request.RequestUri.AbsoluteUri + "/" + id);

            return response;
        }
    }
}
