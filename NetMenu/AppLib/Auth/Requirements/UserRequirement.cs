using Microsoft.AspNetCore.Authorization;

namespace NetMenu.AppLib.Auth.Requirements
{
    public class UserRequirement : IAuthorizationRequirement
    {
        public string[] Roles { get; private set; }

        public UserRequirement(params string[] roles)
        {
            Roles = roles;
        }
    }
}
