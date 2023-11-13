using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CodeCommApi.Models;

namespace CodeCommApi.Dto.DirectMessages.Requests
{
    public class UpdateDirectMessageDto
    {
        

 [Required]
           public MessageType MessageType { get; set; }
           [Required]
        public string MessageBody { get; set; }
        // [Required]
        // public Guid MessageChatId { get; set; }
        // [Required]
        // public Guid MessageSenderId { get; set; }
        // [Required]
        // public Guid MessageReceiverId { get; set; }
        public DateTime MessageTimeDelivered { get; set; } 
        public DateTime MessageTimeSeen { get; set; } 
        public DateTime MessageUpdatedAt { get; set; } 
        public DeletedMessageType MessageIsDeleted { get; set; }
    }
}