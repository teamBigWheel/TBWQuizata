using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoToCO.Startup))]
namespace GoToCO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
