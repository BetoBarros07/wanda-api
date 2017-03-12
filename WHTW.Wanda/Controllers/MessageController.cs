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
        [Route("conversation/{conversationId}/message")]
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
                    Message1 = "Não há respostas disponíveis!",
                    CreatedAt = DateTime.Now
                };
                appContext.Message.Add(botMessage);
                appContext.SaveChanges();
                return Created("/conversation/" + conversationId + "/message", botMessage);
            }
        }

        [HttpGet]
        [Route("conversation/{conversationId}/message")]
        public IHttpActionResult List(Guid conversationId)
        {
            using (var appContext = new AppContext())
            {
                var conversation = appContext.Conversation.FirstOrDefault(a => a.Id == conversationId);
                var messageList = appContext
                    .Message
                    .Where(b => b.ConversationId == conversationId)
                    .OrderBy(b => b.CreatedAt)
                    .ToList()
                    .Select(a => new Message
                    {
                        ConversationId = a.ConversationId,
                        CreatedAt = a.CreatedAt,
                        FromUser = a.FromUser,
                        Id = a.Id,
                        Message1 = a.Message1
                    });
                var retorno = new Conversation
                {
                    FinishDate = conversation.FinishDate,
                    Id = conversation.Id,
                    Message = messageList.ToList(),
                    StartedDate = conversation.StartedDate,
                    UserId = conversation.UserId
                };
                return Ok(retorno);
            }
        }
    }
}