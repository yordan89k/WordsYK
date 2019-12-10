using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WordsYK.Web.Startup))]
namespace WordsYK.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
