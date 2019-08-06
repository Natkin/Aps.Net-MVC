using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dofige.Startup))]
namespace Dofige
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
