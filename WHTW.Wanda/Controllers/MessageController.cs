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
        private const int _finalCount = 1;
        private static int _count = _finalCount;

        private static string[] _messages = new string[]
        {
            "Faz quantos dias?",
            "O seu limite para usar pílula do dia seguinte, já acabou, é melhor consultar um médico!",
        };

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
                    Message1 = _messages[_count--],
                    CreatedAt = DateTime.Now
                };
                if (_count < 0)
                {
                    _count = _finalCount;
                }
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