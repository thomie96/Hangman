using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MuellerThomasMKN_151.Startup))]
namespace MuellerThomasMKN_151
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
