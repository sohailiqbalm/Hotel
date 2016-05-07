using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheHotel.Data.DomainEntities;
using TheHotel.WebUI.Models;
using TheHotel.WebUI.Repository.Hotel;

namespace TheHotel.WebUI.Controllers
{
    public class HotelController : Controller
    {
        IHotelRepository hotelRepository;

        public HotelController(IHotelRepository hotelRepository) 
        {
            this.hotelRepository = hotelRepository;
        }

        // GET: Hotel
        public ActionResult Index()
        {
            // Return the transit schedules as a kendo ui datasource
            IList<HotelModel> hotelModels = Mapper.Map<IList<HotelModel>>(this.hotelRepository.GetAll());
   

            return View(hotelModels);
        }

        public ActionResult Detail(int id)
        {
            // Return the transit schedules as a kendo ui datasource
            HotelModel hotelModel = Mapper.Map<HotelModel>(this.hotelRepository.Get(id));
          
            return View(hotelModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new HotelModel());
        }

        [HttpPost]
        public ActionResult Add(HotelModel hotel)
        {
            int newHotelId = default(int);

            if (hotel != null && this.ModelState.IsValid)
            {
                try
                {
                    // Map to the domain hotel and add to database
                    newHotelId = this.hotelRepository.Add(Mapper.Map<Hotel>(hotel));
                    hotel.Id = newHotelId;
                }
                catch (System.Web.Http.HttpResponseException ex)
                {
                   
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Return the transit schedules as a kendo ui datasource
            HotelModel hotelModel = Mapper.Map<HotelModel>(this.hotelRepository.Get(id));

            return View(hotelModel);
        }

        [HttpPost]
        public ActionResult Edit(HotelModel hotel)
        {

            if (hotel != null && this.ModelState.IsValid)
            {
                try
                {
                    // Map to the domain hotel and add to database
                    this.hotelRepository.Update(Mapper.Map<Hotel>(hotel));
                }
                catch (System.Web.Http.HttpResponseException ex)
                {

                }
            }

            return View();
        }

    }
}