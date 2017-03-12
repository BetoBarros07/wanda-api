using System;
using System.Linq;
using System.Web.Http;

namespace WHTW.Wanda.Controllers
{
    using Models;

    [RoutePrefix("api")]
    [Authorize(Roles = "Patient")]
    public class HospitalController : ApiController
    {
        [HttpGet]
        [Route("especialization/{idEspecialization}/hospital")]
        public IHttpActionResult List(Guid idEspecialization)
        {
            using (var dbContext = new AppContext())
            {
                var list = dbContext.Hospital.Where(a => a.Especialization.Any(b => b.Id == idEspecialization));
                return Ok(list.ToList());
            }
        }
    }
}
