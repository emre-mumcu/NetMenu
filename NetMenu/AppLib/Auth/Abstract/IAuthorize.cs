using Microsoft.AspNetCore.Authentication;

namespace NetMenu.AppLib.Auth.Abstract;

public interface IAuthorize
{
    public AuthenticationTicket GetTicket(string UserName);
}
