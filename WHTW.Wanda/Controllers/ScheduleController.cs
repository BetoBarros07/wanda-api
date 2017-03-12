using System;
using System.Linq;
using System.Web.Http;

namespace WHTW.Wanda.Controllers
{
    using Filters;
    using Models;
    using ViewModels.Schedule;

    [RoutePrefix("api")]
    [Authorize(Roles = "Patient")]
    public class ScheduleController : ApiController
    {
        [HttpGet]
        [Route("schedule")]
        public IHttpActionResult List(string date = null)
        {
            using (var dbContext = new AppContext())
            {
                DateTime dateFilter;
                if (string.IsNullOrEmpty(date))
                    dateFilter = DateTime.Now.Date;
                else
                    dateFilter = Convert.ToDateTime(date.Replace("-", "/"));
                var list = dbContext
                    .Schedule
                    .Include("Hospital")
                    .ToList()
                    .Where(a => a.Date.Date == dateFilter)
                    .Select(a => new
                    {
                        CreatedAt = a.CreatedAt,
                        FromTime = a.Date.TimeOfDay,
                        Hospital = new Hospital
                        {
                            Id = a.Hospital.Id,
                            CreatedAt = a.Hospital.CreatedAt,
                            Name = a.Hospital.Name
                        },
                        Id = a.Id
                    });
                return Ok(list);
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

        [HttpGet]
        [Route("me/schedule")]
        public IHttpActionResult ListPerUser()
        {
            using (var dbContext = new AppContext())
            {
                var userId = Util.GetUserId();
                var list = dbContext.Schedule.Where(a => a.UserId == userId);
                return Ok(list.ToList());
            }
        }
    }
}