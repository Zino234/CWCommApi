using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCommApi.Models;

namespace CodeCommApi.Dto.DirectMessages.Response
{
    public class ReadDirectMessageDto
    {
          public Guid MessageId { get; set; }
        
        public MessageType MessageType { get; set; }
        public string MessageBody { get; set; }
        public Guid MessageChatId { get; set; }
        public Guid MessageSenderId { get; set; }
        public User? MessageSender { get; set; }
        public Guid MessageReceiverId { get; set; }
        public User? MessageReceiver { get; set; }
        public DateTime MessageSentTime { get; set; } 
        public DateTime MessageTimeDelivered { get; set; } 
        public DateTime MessageTimeSeen { get; set; } 
        public DateTime MessageUpdatedAt { get; set; } 
        public DeletedMessageType MessageIsDeleted { get; set; }
    }
}