using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using NetMenu.AppLib.Auth.Requirements;
using NetMenu.AppLib.Configuration.Ext;
using System.Security.Claims;
using System.Security.Permissions;

namespace NetMenu.AppLib.Auth.Handlers
{
    public class UserHandler : AuthorizationHandler<UserRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            //context.Succeed(requirement);
            //return Task.CompletedTask;


            var c = context;


            try
            {


                if (context.Resource is HttpContext httpContext)
                {
                    var endpoint = httpContext.GetEndpoint();
                    var actionDescriptor = endpoint.Metadata.GetMetadata<Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor>();
                }
                else if (context.Resource is Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext mvcContext)
                {
                    // ...
                }



                {   

                    AppUser? appUser = _httpContextAccessor.HttpContext?.Session.GetKey<AppUser>(Literals.SessionKey_AppUser);

                    string[] UserRoles = context.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();
                    
                    if (requirement.Roles.Contains(appUser?.SelectedRole))
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }
                    else
                    {
                        // context.Fail(); redirects users to accessdenied                        
                        context.Fail();
                        return Task.CompletedTask;
                    }
                }
            }
            catch
            {
                throw;
            }
        }        
    }
}