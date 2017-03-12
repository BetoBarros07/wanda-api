using System;
using System.Linq;
using System.Web.Http;

namespace WHTW.Wanda.Controllers
{
    using Filters;
    using Models;
    using ViewModels.Message;

    [RoutePrefix("api")]
    [Authorize(Roles = "Patient")]
    public class MessageController : ApiController
    {
        [HttpPost]
        [Route("conversartion/{conversationId}/message")]
        [ModelStateValid]
        public IHttpActionResult Create(Guid conversationId, CreateMessage model)
        {
            using (var appContext = new AppContext())
            {
                appContext.Message.Add(model.ToEntity(conversationId));
                var botMessage = new Message
                {
                    Id = Guid.NewGuid(),
                    ConversationId = conversationId,
                    FromUser = false,
                    Message1 = "Não há respostas disponíveis!"
                };
                appContext.Message.Add(botMessage);
                appContext.SaveChanges();
                return Created("/conversation/" + conversationId + "/message", botMessage);
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