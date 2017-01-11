using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MobileAppDocDBOfflineSyncSampleService.Startup))]

namespace MobileAppDocDBOfflineSyncSampleService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}