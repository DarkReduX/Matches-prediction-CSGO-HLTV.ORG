using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSGO_match_result_prediction_курсовая_практика_.Startup))]
namespace CSGO_match_result_prediction_курсовая_практика_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
