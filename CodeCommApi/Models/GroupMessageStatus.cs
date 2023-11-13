using Microsoft.EntityFrameworkCore;

namespace CodeCommApi.Models
{
    [PrimaryKey("GroupMessageId","UserId")]
    public class GroupMessageStatus
    {
        
        public Guid GroupMessageId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DeliveredAt { get; init; }
        public DateTime SeenAt { get; init; }
    }
}