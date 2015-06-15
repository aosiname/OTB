using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OTB_HR_System.Startup))]
namespace OTB_HR_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
