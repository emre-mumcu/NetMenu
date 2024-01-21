using Microsoft.AspNetCore.Authentication;

namespace NetMenu.AppLib.Auth.Abstract;

public interface IAuthorize
{
    public Task<AuthenticationTicket> GetTicket(string UserName);
}
