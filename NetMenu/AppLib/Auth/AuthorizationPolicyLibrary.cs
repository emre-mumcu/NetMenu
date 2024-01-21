using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using NetMenu.AppLib.Auth.Requirements;

namespace NetMenu.AppLib.Auth
{
    public static class AuthorizationPolicyLibrary
    {
        public static AuthorizationPolicy defaultPolicy = new AuthorizationPolicyBuilder()
           .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
           .RequireAuthenticatedUser()
           .Build();

        public static AuthorizationPolicy fallbackPolicy = new AuthorizationPolicyBuilder()
           .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
           .RequireAuthenticatedUser()
           .Build();

        public static AuthorizationPolicy GuestPolicy = new AuthorizationPolicyBuilder()
           .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
           .RequireAuthenticatedUser()
           .AddRequirements(new BaseRequirement())
           .AddRequirements(new UserRequirement(new string[] {
                AppRoles.Guest.ToString()
            }))
           .Build();

        public static AuthorizationPolicy UserPolicy = new AuthorizationPolicyBuilder()
           .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
           .RequireAuthenticatedUser()
           .AddRequirements(new BaseRequirement())
           .AddRequirements(new UserRequirement(new string[] {
                AppRoles.Developer.ToString(),
                AppRoles.Administrator.ToString(),
                AppRoles.Supervisor.ToString(),
                AppRoles.User.ToString()
            }))
           .Build();

        public static AuthorizationPolicy AdminPolicy = new AuthorizationPolicyBuilder()
           .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
           .RequireAuthenticatedUser()
           .AddRequirements(new BaseRequirement())
           .AddRequirements(new UserRequirement(new string[] {
               AppRoles.Developer.ToString(),
               AppRoles.Administrator.ToString()
            }))
           //.RequireRole("USER")
           //.RequireAssertion(ctx => { return ctx.User.HasClaim("editor", "contents") || ctx.User.HasClaim("level", "senior"); })
           .Build();
    }

    // https://github.com/aspnet/Security/blob/master/src/Microsoft.AspNetCore.Authorization.Policy/PolicyEvaluator.cs
    // https://github.com/dotnet/aspnetcore/blob/main/src/Shared/SecurityHelper/SecurityHelper.cs
    // https://github.com/dotnet/aspnetcore/tree/main/src
}