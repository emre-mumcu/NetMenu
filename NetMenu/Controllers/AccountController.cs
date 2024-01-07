using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using NetMenu.AppLib;
using NetMenu.AppLib.Auth.Abstract;
using System.Net.Sockets;
using NetMenu.AppLib.Configuration.Ext;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace NetMenu.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authenticator;
        private readonly IAuthorize _authorizer;

        public AccountController(IAuthenticate authenticator, IAuthorize authorizer)
        {
            _authenticator = authenticator;
            _authorizer = authorizer;
        }

        public async Task<IActionResult> Login()
        {
            string UserName = "John";
            string Password = "123456";

            bool isAuthenticated = _authenticator.AuthenticateUser(UserName, Password);

            if(isAuthenticated)
            {
                var ticket = _authorizer.GetTicket(UserName);

                HttpContext.Session.CreateUserSession(ticket.Principal);

                await HttpContext.SignInAsync(
                    ticket.AuthenticationScheme,
                    ticket.Principal,
                    ticket.Properties
                );

                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            else
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
        }

        //public IActionResult AccessDenied() => new JsonResult(
        //    new { StatusCode = StatusCodes.Status403Forbidden, StatusMessage = "Forbidden" }
        //);

        public IActionResult AccessDenied()
        {
            bool IsUserSessionValid = HttpContext.Session.ValidateUserSession();

            if(!IsUserSessionValid)
            {
                return RedirectToAction("Logout");
            }

            return new JsonResult(
                new { StatusCode = StatusCodes.Status403Forbidden, StatusMessage = "Forbidden" }
            );
        }


        public async Task<IActionResult> Logout()
        {
            HttpContext.User = new ClaimsPrincipal();

            HttpContext.Session.RemoveUserSession();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var session_cookie = HttpContext.Request.Cookies[Literals.Session_Cookie_Name];
            if (session_cookie != null)
            {
                var options = new CookieOptions { Expires = DateTime.Now.AddDays(-1) };
                HttpContext.Response.Cookies.Append(Literals.Session_Cookie_Name, session_cookie, options);
            }

            var auth_cookie = HttpContext.Request.Cookies[Literals.Auth_Cookie_Name];
            if (auth_cookie != null)
            {
                var options = new CookieOptions { Expires = DateTime.Now.AddDays(-1) };
                HttpContext.Response.Cookies.Append(Literals.Session_Cookie_Name, auth_cookie, options);
            }

            return new RedirectResult(Url.Content("~/"));
        }
    }
}
