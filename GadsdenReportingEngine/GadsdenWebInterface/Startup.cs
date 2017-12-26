using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GadsdenWebInterface.Startup))]
namespace GadsdenWebInterface
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
