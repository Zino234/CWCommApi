using System.ComponentModel.DataAnnotations;
using CodeCommApi.Models;

namespace CodeCommApi.Dto.GroupMessage.Request
{
    public class CreateGroupMessageDto
    {
        public MessageType MessageType { get; set; } = MessageType.Text;

        [Required]
        public String MessageBody { get; set; }

        //GROUP ID FOR THE GROUP WHICH THE MESSAGE IS GOING TO BE SENT INTO
        [Required]
        public Guid GroupId { get; set; }

    }
}
