using NetMenu.AppLib.Configuration.Ext;

namespace NetMenu.AppLib.Configuration
{
    public static class ConfigureServices
    {
        public static WebApplicationBuilder _ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services._InitMVC();

            builder.Services._AddCookieConfiguration();

            builder.Services._AddSession();

            builder.Services._AddAuthentication();

            builder.Services._AddAuthorization();

            builder.Services.AddHttpContextAccessor();

            return builder;
        }
    }
}
