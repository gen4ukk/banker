using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Banker.Startup))]
namespace Banker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
