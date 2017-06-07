using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DapperT2.Startup))]
namespace DapperT2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
