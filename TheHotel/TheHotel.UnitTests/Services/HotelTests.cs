using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TheHotel.Service.Services;
using System.Collections.Generic;
using TheHotel.Data.DomainEntities;
using System.Linq;
using TheHotel.Infrastructure.Repository;
 
namespace TheHotel.UnitTests.Services
{
    [TestClass]
    public class HotelTests
    {
        Mock<IHotelRepository> hotelRepository;

        public HotelTests() 
        {
            hotelRepository = new Mock<IHotelRepository>();
        }

        [TestMethod]
        public void ShouldReturnHotelSuccessfullyWhenGivenValidId()
        {
            List<Hotel> hotels = GetStubbedHotels();

            hotelRepository.Setup(m => m.Get(1)).Returns(GetStubbedHotels().FirstOrDefault(x => x.Id == 1));

            HotelService service = new HotelService(hotelRepository.Object);

            var hotel = service.GetHotel(1);

            Assert.AreEqual(1, hotel.Id);
        }

        private List<Hotel> GetStubbedHotels() 
        {
            List<Hotel> hotels = new List<Hotel>();

            hotels.Add(new Hotel() { Id = 1, Owner = "umer", Name = "pc" });
            hotels.Add(new Hotel() { Id = 2, Owner = "umer", Name = "marriot" });

            return hotels;
        }
    }
}
