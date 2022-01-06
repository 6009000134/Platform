using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyPlatform.Startup))]
namespace MyPlatform
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
