using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheHotel.WebUI.Startup))]
namespace TheHotel.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
