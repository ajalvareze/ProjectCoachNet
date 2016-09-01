using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectCoach.Startup))]
namespace ProjectCoach
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
