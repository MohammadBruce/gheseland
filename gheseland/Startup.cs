using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(gheseland.Startup))]
namespace gheseland
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
