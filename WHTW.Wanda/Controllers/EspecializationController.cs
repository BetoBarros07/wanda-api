using System.Linq;
using System.Web.Http;

namespace WHTW.Wanda.Controllers
{
    using Models;

    [RoutePrefix("api")]
    [Authorize(Roles = "Patient")]
    public class EspecializationController : ApiController
    {
        [HttpGet]
        [Route("especialization")]
        public IHttpActionResult List()
        {
            using (var dbContext = new AppContext())
            {
                var list = dbContext.Especialization.ToList();
                return Ok(list);
            }
        }
    }
}
