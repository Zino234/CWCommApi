using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommApi.Data;
using CodeCommApi.Dependencies.interfaces;
using CodeCommApi.Dependencies.Interfaces;
using CodeCommApi.Dto.DirectMessages.Requests;
using CodeCommApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeCommApi.Dependencies.Services
{
    public class DirectMessageService : IDirectMessageService
    {
        private readonly CodeCommDbContext _context;
        private readonly IMapper _mapper;

        private readonly IUserService _user;
        private readonly IChatService _chat;

        public DirectMessageService(
            CodeCommDbContext context,
            IMapper mapper,
            IChatService chat,
            IUserService user
        )
        {
            _chat = chat;
            _user = user;
            _context = context;
            _mapper = mapper;
        }

        public async Task<DirectMessage> CreateDirectMessage(
            Guid ChatId,
            Guid SenderId,
            Guid ReceiverId,
            CreateDirectMessageDto dto
        )
        {
            var chat = await _chat.GetChatById(ChatId);
            bool validChats =
                (chat.UserID1 == SenderId && chat.UserID2 == ReceiverId)
                || (chat.UserID1 == ReceiverId && chat.UserID2 == SenderId);

            if (!validChats || chat == null)
            {
                return null;
            }
            try
            {
                var sender = await _user.FindUser(SenderId);
                var receiver = await _user.FindUser(ReceiverId);
                var message = _mapper.Map<DirectMessage>(dto);
                message.MessageId=Guid.NewGuid();
                message.MessageChatId = ChatId;
                message.MessageSenderId = SenderId;
                // message.MessageSender = sender;
                message.MessageReceiverId = ReceiverId;
                // message.MessageReceiver = receiver;
                await _context.DirectMessages.AddAsync(message);
                await _context.SaveChangesAsync();
                return message;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<DirectMessage>> GetChatMessages(Guid ChatId)
        {
            var messages = await _context.DirectMessages
                .Where(x => x.MessageChatId == ChatId)
                .ToListAsync();
            if (messages == null)
            {
                return null;
            }
            return messages;
        }

        public async Task<DirectMessage> GetSingleMessage(Guid MessageId)
        {
            var message = await _context.DirectMessages.FirstOrDefaultAsync(
                x => x.MessageId == MessageId
            );
            if (message == null)
            {
                return null;
            }
            return message;
        }

        public async Task<DirectMessage> UpdateMessage(Guid MessageId, UpdateDirectMessageDto dto)
        {
            var message = await GetSingleMessage(MessageId);
            if (message == null)
            {
                return null;
            }
            message.MessageType=dto.MessageType;
            message.MessageBody=dto.MessageBody;
            message.MessageTimeDelivered=dto.MessageTimeDelivered;
            message.MessageTimeSeen=dto.MessageTimeSeen;
            message.MessageUpdatedAt=DateTime.Now;
            _context.Entry(message).State=EntityState.Modified;
            await _context.SaveChangesAsync();

           return message;
        }

        public async Task<bool> DeleteMessage(Guid MessageId)
        {
            var message = await GetSingleMessage(MessageId);
            if (message == null)
            {
                return false;
            }
            
            _context.DirectMessages.Remove(message);
            _context.Entry(message).State=EntityState.Deleted;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
