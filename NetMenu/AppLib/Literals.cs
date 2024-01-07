namespace NetMenu.AppLib
{
    public static class Literals
    {
        public const string SessionKey_AppUser = "AppUser";

        public const string Session_Cookie_Name = "app.cookie.session";
        public static TimeSpan Session_IdleTimeout = TimeSpan.FromMinutes(20);

        public const string Auth_Cookie_Name = "app.cookie.authentication";
        public const string Auth_Cookie_LoginPath = "/Account/Login";
        public const string Auth_Cookie_LogoutPath = "/Account/Logout";
        public const string Auth_Cookie_AccessDeniedPath = "/Account/AccessDenied";
        public const string Auth_Cookie_ClaimsIssuer = "app.cookie.issuer";
        public const string Auth_Cookie_ReturnUrlParameter = "ReturnUrl"; // Değiştirme kullanılıyor!!!
        public const string Auth_Cookie_RedirectUrl = "RedirectUrl";
        public const int Auth_Cookie_ExpireTimeSpan = 20;
    }
}
