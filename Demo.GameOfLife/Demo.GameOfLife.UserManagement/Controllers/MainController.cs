using System.Web.Http;

namespace Demo.GameOfLife.UserManagement.Controllers
{
    [ServiceRequestActionFilter]
    public class MainController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("System up.");
        }
    }
}