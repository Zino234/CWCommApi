using System.ComponentModel.DataAnnotations;
using CodeCommApi.Models;

namespace CodeCommApi.Dto.GroupMessage.Response
{
    public class ReadGroupMessageDto
    {
        public Guid MessageId { get; set; }
        public MessageType MessageType { get; set; } = MessageType.Text;

        [Required]
        public String MessageBody { get; set; }

        //GROUP ID FOR THE GROUP WHICH THE MESSAGE IS GOING TO BE SENT INTO
        [Required]
        public Guid GroupId { get; set; }

        //USER ID FOR THE SENDER OF THE  MESSAGE
        [Required]
        public Guid MessageSentBy { get; set; }
        public DateTime MessageTimeSent { get; init; }
        public DateTime? EditedAt { get; set; } = null;
        public DeletedMessageType MessageIsDeleted { get; set; } = DeletedMessageType.NotDeleted;
    }
}
