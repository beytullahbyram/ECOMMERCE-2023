using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ECOMMERCE_2023.Startup))]
namespace ECOMMERCE_2023
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
