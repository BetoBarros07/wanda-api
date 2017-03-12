using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WHTW.Wanda.Controllers
{
    using Models;

    [RoutePrefix("api")]
    public class ScheduleController : ApiController
    {
        [HttpGet]
        [Route("hospital/{hospitalId}/schedule")]
        public IHttpActionResult List(Guid hospitalId)
        {
            using (var dbContext = new AppContext())
            {
                return Ok();
            }
        }
    }
}
