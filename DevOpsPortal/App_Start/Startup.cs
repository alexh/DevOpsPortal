using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevOpsPortal.Startup))]

namespace DevOpsPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}