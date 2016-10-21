using System.Threading.Tasks;
using System.Web.Http;

namespace Demo.GameOfLife.UserManagement.Controllers
{
    [ServiceRequestActionFilter]
    public class UserStatsController : AuthenticationServiceBaseController
    {
        [HttpGet]
        public async Task<IHttpActionResult> GetCurrentUserCount()
        {
            var result = await GetUserStatsServiceInstance().CurrentlyLoggedInUserCount();
            return Ok(result);
        }
    }
}