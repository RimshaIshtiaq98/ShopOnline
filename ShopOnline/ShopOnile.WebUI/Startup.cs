using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopOnline.WebUI.Startup))]
namespace ShopOnline.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
