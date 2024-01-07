using NetMenu.AppLib.Auth.Abstract;

namespace NetMenu.AppLib.Auth.Providers;

public class DevAuthenticate : IAuthenticate
{
    public bool AuthenticateUser(string UserName, string Password)
    {
        return true;
    }
}