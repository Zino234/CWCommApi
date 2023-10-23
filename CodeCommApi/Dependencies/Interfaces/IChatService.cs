using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCommApi.Dto;
using CodeCommApi.Dto.Chats.Requests;
using CodeCommApi.Models;

namespace CodeCommApi.Dependencies.interfaces
{
    public interface IChatService
    {
        Task<Chat> CreateChat(CreateChatDto dto);
        Task<List<Chat>> GetUserChats(Guid UserId);
        Task<Chat> GetChatById(Guid ChatId);
        Task<Chat> GetChatByUsers(Guid User1,Guid User2);
        Task<bool> DeleteChat(Guid ChatId);
    }
}