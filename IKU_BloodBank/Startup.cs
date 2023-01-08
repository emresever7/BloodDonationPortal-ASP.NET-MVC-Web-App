using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IKU_BloodBank.Startup))]
namespace IKU_BloodBank
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
