using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using TheHotel.WebUI.Repository.Hotel;
using Castle.Windsor.Installer;

namespace TheHotel.WebUI.Installers
{
    public class WindsorControllerFactory : System.Web.Mvc.DefaultControllerFactory
    {
        private IKernel _kernel;
        public WindsorControllerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }
        public override void ReleaseController(IController controller)
        {
            _kernel.ReleaseComponent(controller);
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The Windsor Controller at path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }
            return (IController)_kernel.Resolve(controllerType);
        }
    }

    public static class IOCInstaller
    {
        private static IWindsorContainer _container;

        public static void Setup()
        {
            _container = new WindsorContainer().Install(FromAssembly.This());

            WindsorControllerFactory controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }

    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store) 
        {
            container.AddFacility<TypedFactoryFacility>();

            container.Register(
                Classes.FromThisAssembly()
                .BasedOn<IController>()
                .LifestylePerWebRequest());

             string serviceEndpoint = ConfigurationManager.AppSettings["WebApi.Services.Endpoint"];

             // Register any repositories
             container.Register(
                 Component.For<IHotelRepository>().ImplementedBy(typeof(HotelRepository))
                 .DependsOn(Property.ForKey("serviceEndpoint").Eq(serviceEndpoint)).LifeStyle.Transient);

        }
    }
}