using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalChallenge.Startup))]
namespace FinalChallenge
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
