
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Demo.GameOfLife.WebApp.Controllers
{
    public class DemoGameController : AuthenticaticatedBaseController
    {
        // GET: DemoGame
        public async Task<ActionResult> Index()
        {
            var token = HttpContext.Request.QueryString["token"];
            if (string.IsNullOrEmpty(token) || !HttpContext.User.Identity.IsAuthenticated)
                return new HttpUnauthorizedResult();

            var isValid = await GetAuthServiceInstance().ValidateUserSessionToken(HttpContext.User.Identity.Name, Guid.Parse(token));
            if(!isValid)
                return new HttpUnauthorizedResult();

            return View();
        }
    }
}