using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Metropole.Startup))]
namespace Metropole
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
