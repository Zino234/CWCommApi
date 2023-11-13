using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCommApi.Dto.DirectMessages.Requests;
using CodeCommApi.Models;

namespace CodeCommApi.Dependencies.Interfaces
{
    public interface IDirectMessageService
    {
        Task<DirectMessage> CreateDirectMessage(
            Guid ChatId,
            Guid SenderId,
            Guid ReceiverId,
            CreateDirectMessageDto dto
        );

        Task<List<DirectMessage>> GetChatMessages(Guid ChatId);

        Task<DirectMessage> GetSingleMessage(Guid MessageId);

        Task<DirectMessage> UpdateMessage(Guid MessageId, UpdateDirectMessageDto dto);

        Task<bool> DeleteMessage(Guid MessageId);
    }
}
