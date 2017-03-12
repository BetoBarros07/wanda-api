using System;
using System.Linq;
using System.Web.Http;

namespace WHTW.Wanda.Controllers
{
    using Filters;
    using Models;
    using ViewModels.Message;

    [Authorize]
    [RoutePrefix("api")]
    public class MessageController : ApiController
    {
        [HttpPost]
        [Route("conversartion/{convesationId}/message")]
        [ModelStateValid]
        public IHttpActionResult Create(Guid convesationId, CreateMessage model)
        {
            using (var appContext = new AppContext())
            {
                appContext.Message.Add(model.ToEntity(convesationId));
                appContext.SaveChanges();
                return Created("/conversation/" + convesationId + "message", new
                {

                });
            }
        }

        [HttpGet]
        [Route("conversartion/{convesationId}/message")]
        public IHttpActionResult List(Guid convesationId)
        {
            using (var appContext = new AppContext())
            {
                var conversation = appContext.Conversation.Include("Message").FirstOrDefault(a => a.Id == convesationId);
                return Ok(conversation);
            }
        }
    }
}