using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopOnile.WebUI.Startup))]
namespace ShopOnile.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
