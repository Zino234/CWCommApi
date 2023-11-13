using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCommApi.Models
{
    public class DirectMessage
    {
        [Key]
        public Guid MessageId { get; set; }
        
        public MessageType MessageType { get; set; }
        public string MessageBody { get; set; }
        public Guid MessageChatId { get; set; }
        // [ForeignKey("MessageSender")]
        public Guid MessageSenderId { get; set; }
        // public User? MessageSender { get; set; }
        // [ForeignKey("MessageReceiver")]
        public Guid MessageReceiverId { get; set; }
        // public User? MessageReceiver { get; set; }
        public DateTime MessageSentTime { get; set; } = DateTime.Now;
        public DateTime MessageTimeDelivered { get; set; } = DateTime.MinValue;
        public DateTime MessageTimeSeen { get; set; } = DateTime.MinValue;
        public DateTime MessageUpdatedAt { get; set; } = DateTime.MinValue;
        public DeletedMessageType MessageIsDeleted { get; set; } = DeletedMessageType.NotDeleted;
    }
}
