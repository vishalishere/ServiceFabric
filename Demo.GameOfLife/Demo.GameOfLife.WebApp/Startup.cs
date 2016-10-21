using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Demo.GameOfLife.WebApp.Startup))]
namespace Demo.GameOfLife.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
