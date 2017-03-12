using System;
using System.ComponentModel.DataAnnotations;

namespace WHTW.Wanda.ViewModels.Message
{
    public class CreateMessage
    {
        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }

        public Models.Message ToEntity(Guid convesationId)
        {
            return new Models.Message
            {
                Id = Guid.NewGuid(),
                ConversationId = convesationId,
                Message1 = Message,
                FromUser = true,
                CreatedAt = DateTime.Now
            };
        }
    }
}