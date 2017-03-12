using System;
using System.Linq;
using System.Web.Http;

namespace WHTW.Wanda.Controllers
{
    using Models;

    [RoutePrefix("api")]
    [Authorize(Roles = "Patient")]
    public class ConversationController : ApiController
    {
        [HttpGet]
        [Route("conversation")]
        public IHttpActionResult List()
        {
            var loggedUserId = Util.GetUserId();
            using (var dbContext = new AppContext())
            {
                var list = dbContext
                    .Conversation
                    .Where(a => a.UserId == loggedUserId)
                    .ToList()
                    .Select(a => new Conversation
                    {
                        FinishDate = a.FinishDate,
                        Id = a.Id,
                        StartedDate = a.StartedDate,
                        UserId = a.UserId
                    });
                return Ok(list);
            }
        }

        [HttpPost]
        [Route("conversation")]
        public IHttpActionResult Create()
        {
            var loggedUserId = Util.GetUserId();
            using (var dbContext = new AppContext())
            {
                var conversation = new Conversation
                {
                    Id = Guid.NewGuid(),
                    StartedDate = DateTime.Now,
                    UserId = loggedUserId
                };
                dbContext.Conversation.Add(conversation);
                dbContext.SaveChanges();
                return Created("/conversation/" + conversation.Id + "/messages", conversation);
            }
        }

        [HttpPatch]
        [Route("conversation/{id}")]
        public IHttpActionResult FinishConvesartion(Guid id)
        {
            using (var dbContext = new AppContext())
            {
                var conversation = dbContext.Conversation.Find(id);
                if (conversation == null)
                {
                    return NotFound();
                }
                conversation.FinishDate = DateTime.Now;
                dbContext.SaveChanges();
                return Ok();
            }
        }
    }
}