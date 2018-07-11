using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GymBooking_Ver0._1.Startup))]
namespace GymBooking_Ver0._1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
