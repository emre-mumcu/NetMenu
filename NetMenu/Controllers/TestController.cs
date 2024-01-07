using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetMenu.AppLib.Auth;
using NetMenu.AppLib;

namespace NetMenu.Controllers
{
    [Authorize(Policy = nameof(AuthorizationPolicyLibrary.UserPolicy))]
    //[MenuItem]
    public class TestController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
