using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TheHotel.Common.Core;
using TheHotel.WebApi.IoC;
 

namespace TheHotel.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IWindsorContainer container = RegisterWindsorContainer();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private IWindsorContainer RegisterWindsorContainer() 
        {
            IWindsorContainer container = new WindsorContainer().Install(FromAssembly.This());

            // Setup the MVC DependencyResolver
            DependencyResolver.SetResolver(new WindsorDependencyResolver(container.Kernel));

            // Use an adapter to setup the WebApi Dependency 
            GlobalConfiguration.Configuration.DependencyResolver = DependencyResolver.Current.ToServiceResolver();

            return container;
        }

        }
}
