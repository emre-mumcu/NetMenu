using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetMenu.AppLib;
using NetMenu.AppLib.Auth;

namespace NetMenu.Controllers
{
    [Authorize(Policy = nameof(AuthorizationPolicyLibrary.UserPolicy))]
    [MenuItem]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu1()
        {
            return new JsonResult(new { From = "Menu1" });
        }

        [Authorize(Policy = nameof(AuthorizationPolicyLibrary.AdminPolicy))]
        [MenuItem(_ParentText: "Home", _MenuText: "Banka Bilgileri", _ParentIndex: 10, _IsSingle: false, _ParentIconClass: "fa fa-cogs")]
        public IActionResult Menu2()
        {
            return new JsonResult(new { From = "Menu2" });
        }
    }
}
