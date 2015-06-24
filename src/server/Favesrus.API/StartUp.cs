using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Favesrus.Web.Startup))]
namespace Favesrus.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
