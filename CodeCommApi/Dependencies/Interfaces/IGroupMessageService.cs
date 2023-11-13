using CodeCommApi.Dto.GroupMessage.Request;
using CodeCommApi.Models;

namespace CodeCommApi.Dependencies.Interfaces
{
    public interface IGroupMessageService
    {
        Task<GroupMessage> CreateGroupMessage(Guid UserId,CreateGroupMessageDto dto);

        Task<List<GroupMessage>> GetAllGroupMessages(Guid groupId);
        Task<GroupMessage> GetMessageById(Guid MessageId);
        Task<GroupMessage> UpdateMessage(Guid MessageId,UpdateGroupMessageDto dto);
        Task<bool> DeleteMessage(Guid MessageId);

        Task<GroupMessage> DeliverMessageToUser(Guid MessageId, Guid UserId);

        Task<GroupMessage> UserSeenMessage(Guid MessageId, Guid UserId);
    }
}
