using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XpandBlog.Web.Startup))]
namespace XpandBlog.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var unityContainer = BootstrapperMVC.Initialise();

            ConfigureAuth(app);
        }
    }
}
