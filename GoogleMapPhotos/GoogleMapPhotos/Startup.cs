using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoogleMapPhotos.Startup))]
namespace GoogleMapPhotos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
