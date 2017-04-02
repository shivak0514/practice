using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DemoApplication.Startup))]
namespace DemoApplication
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
