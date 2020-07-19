using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Green_Collector.Startup))]
namespace Green_Collector
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
