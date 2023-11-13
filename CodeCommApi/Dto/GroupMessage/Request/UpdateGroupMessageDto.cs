using System.ComponentModel.DataAnnotations;
using CodeCommApi.Models;

namespace CodeCommApi.Dto.GroupMessage.Request
{
    public class UpdateGroupMessageDto
    {
        public MessageType MessageType { get; set; } = MessageType.Text;

        [Required]
        public String MessageBody { get; set; }
        
        [Required]
        public DateTime? EditedAt { get; set; } = DateTime.Now;
        public DeletedMessageType MessageIsDeleted { get; set; } = DeletedMessageType.NotDeleted;
    }
}
