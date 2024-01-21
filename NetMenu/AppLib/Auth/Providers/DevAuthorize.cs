using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using NetMenu.AppLib.Auth.Abstract;
using System.Security.Claims;

namespace NetMenu.AppLib.Auth.Providers;

public class DevAuthorize : IAuthorize
{
    public DevAuthorize() { }

    private async Task<ClaimsPrincipal> GetPrincipal(string userId)
    {
        List<Claim> userClaims = new List<Claim>();

        await Task.Run(() => {
            userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, "Name"),
                new Claim(ClaimTypes.Surname, "Surname"),
                new Claim(ClaimTypes.Role, AppRoles.Guest.ToString()),
                new Claim(ClaimTypes.Role, AppRoles.User.ToString())
            }; 
        });        

        return new ClaimsPrincipal(new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme));
    }

    private async Task<AuthenticationProperties> GetProperties() => new AuthenticationProperties
    {
        AllowRefresh = true,
        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(Literals.Auth_Cookie_ExpireTimeSpan),
        IsPersistent = true,
        IssuedUtc = DateTimeOffset.UtcNow,
        RedirectUri = Literals.Auth_Cookie_RedirectUrl
    };

    public async Task<AuthenticationTicket> GetTicket(string userId) => 
        new AuthenticationTicket(await GetPrincipal(userId), 
            await GetProperties(), 
            CookieAuthenticationDefaults.AuthenticationScheme);
}
