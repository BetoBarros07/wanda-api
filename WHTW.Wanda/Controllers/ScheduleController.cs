using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WHTW.Wanda.Controllers
{
    using Filters;
    using Models;
    using ViewModels.Schedule;

    [Authorize]
    [RoutePrefix("api")]
    public class ScheduleController : ApiController
    {
        [HttpGet]
        [Route("hospital/{hospitalId}/schedule")]
        public IHttpActionResult List(Guid hospitalId, string date = null)
        {
            using (var dbContext = new AppContext())
            {
                DateTime dateFilter;
                if (string.IsNullOrEmpty(date))
                    dateFilter = DateTime.Now.Date;
                else
                    dateFilter = Convert.ToDateTime(date);
                return Ok(dbContext.Schedule.Where(a => a.Date.Date == dateFilter).ToList());
            }
        }

        [HttpPost]
        [Route("hospital/{hospitalId}/schedule")]
        [ModelStateValid]
        public IHttpActionResult Create(Guid hospitalId, CreateSchedule model)
        {
            using (var dbContext = new AppContext())
            {
                var schedule = new Schedule
                {
                    CreatedAt = DateTime.Now,
                    Date = model.Date,
                    HospitalId = hospitalId,
                    Id = Guid.NewGuid(),
                    UserId = Util.GetUserId()
                };
                dbContext.Schedule.Add(schedule);
                return Created("/me/schedules", schedule);
            }
        }
    }
}