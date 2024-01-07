namespace NetMenu.AppLib.Auth.Abstract;

public interface IAuthenticate
{
    public bool AuthenticateUser(string UserName, string Password);
}
