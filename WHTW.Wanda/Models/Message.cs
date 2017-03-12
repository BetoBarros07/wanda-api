namespace WHTW.Wanda.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        public Guid Id { get; set; }

        [Column("Message")]
        [Required]
        public string Message1 { get; set; }

        public Guid ConversationId { get; set; }

        public bool FromUser { get; set; }

        public virtual Conversation Conversation { get; set; }
    }
}
