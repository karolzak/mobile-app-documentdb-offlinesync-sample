using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MobileAppDocDBOfflineSyncSample.API.Startup))]

namespace MobileAppDocDBOfflineSyncSample.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}