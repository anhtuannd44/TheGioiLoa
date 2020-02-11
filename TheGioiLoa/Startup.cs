using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheGioiLoa.Startup))]
namespace TheGioiLoa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
