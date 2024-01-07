using Microsoft.AspNetCore.Authorization;
using NetMenu.AppLib.Auth.Abstract;
using NetMenu.AppLib.Auth;
using NetMenu.AppLib.Auth.Providers;
using NetMenu.AppLib.Auth.Handlers;

namespace NetMenu.AppLib.Configuration.Ext
{
    public static class Authorization
    {
        public static IServiceCollection _AddAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = AuthorizationPolicyLibrary.defaultPolicy;
                options.FallbackPolicy = AuthorizationPolicyLibrary.fallbackPolicy;
                options.InvokeHandlersAfterFailure = false;

                // Using inline policy:
                // options.AddPolicy("LiteralAdminPolicy", policy => policy.RequireClaim("Role", "Admin"));     

                // Using AuthorizationPolicyLibrary:
                options.AddPolicy(nameof(AuthorizationPolicyLibrary.AdminPolicy), AuthorizationPolicyLibrary.AdminPolicy);
                options.AddPolicy(nameof(AuthorizationPolicyLibrary.UserPolicy), AuthorizationPolicyLibrary.UserPolicy);
                options.AddPolicy(nameof(AuthorizationPolicyLibrary.GuestPolicy), AuthorizationPolicyLibrary.GuestPolicy);
            });

            services.AddSingleton<IAuthorize, DevAuthorize>();

            services.AddSingleton<IAuthorizationHandler, BaseHandler>();

            services.AddSingleton<IAuthorizationHandler, UserHandler>();

            return services;
        }

        public static IApplicationBuilder _UseAuthorization(this WebApplication app)
        {
            app.UseAuthorization();
            return app;
        }
    }
}
