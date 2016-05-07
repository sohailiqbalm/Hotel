using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Http.Controllers;
using TheHotel.Infrastructure.Repository;
using TheHotel.Service.Services;
using System.Web.Mvc;
using Castle.Windsor.Installer;

namespace TheHotel.WebApi.IoC
{

    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store) 
        {
            container.Register(
                Classes.FromThisAssembly()
                .BasedOn<IHttpController>()
                .LifestylePerWebRequest());

            container.Register(
                Component.For<IHotelRepository>().ImplementedBy<HotelRepository>());

            container.Register(
                Component.For<IHotelService>().ImplementedBy<HotelService>());


        }
    }
}