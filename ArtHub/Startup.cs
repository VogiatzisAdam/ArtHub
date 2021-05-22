using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArtHub.Startup))]
namespace ArtHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
