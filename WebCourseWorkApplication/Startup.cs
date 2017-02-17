using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebCourseWorkApplication.Startup))]
namespace WebCourseWorkApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
