using Microsoft.AspNetCore.Authentication.Cookies;
using NetMenu.AppLib.Auth.Abstract;
using NetMenu.AppLib.Auth.Providers;

namespace NetMenu.AppLib.Configuration.Ext;

public static class Authentication
{
    public static IServiceCollection _AddAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.Cookie.Name = Literals.Auth_Cookie_Name;
            options.LoginPath = Literals.Auth_Cookie_LoginPath;
            options.LogoutPath = Literals.Auth_Cookie_LogoutPath;
            options.AccessDeniedPath = Literals.Auth_Cookie_AccessDeniedPath;
            options.ClaimsIssuer = Literals.Auth_Cookie_ClaimsIssuer;
            options.ReturnUrlParameter = Literals.Auth_Cookie_ReturnUrlParameter;
            options.SlidingExpiration = true;
            options.Cookie.HttpOnly = true; // False causes xss vulnerability !!!					
            options.ExpireTimeSpan = TimeSpan.FromMinutes(Literals.Auth_Cookie_ExpireTimeSpan); // This is for the ticket NOT cookie!!!
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.Validate();
            options.EventsType = typeof(AppCookieAuthenticationEvents);
            options.Events = new CookieAuthenticationEvents
            {
                // OnValidatePrincipal = (context) => Task.CompletedTask,
                // OnRedirectToLogin = (context) => Task.CompletedTask
            };
        });

        services.AddScoped<AppCookieAuthenticationEvents>();

        services.AddSingleton<IAuthenticate, DevAuthenticate>();

        return services;
    }

    public static IApplicationBuilder _UseAuthentication(this WebApplication app)
    {
        app.UseAuthentication();

        return app;
    }
}
