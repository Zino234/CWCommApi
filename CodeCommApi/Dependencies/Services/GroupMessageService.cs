using System;
using AutoMapper;
using CodeCommApi.Data;
using CodeCommApi.Dependencies.Interfaces;
using CodeCommApi.Dto.GroupMessage.Request;
using CodeCommApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeCommApi.Dependencies.Services
{
    public class GroupMessageService : IGroupMessageService
    {
        private readonly CodeCommDbContext _context;
        private IUserService _user;
        private readonly IMapper _mapper;

        private IGroupService _group;

        public GroupMessageService(
            IUserService user,
            IMapper mapper,
            IGroupService group,
            CodeCommDbContext context
        )
        {
            _context = context;
            _user = user;
            _mapper = mapper;
            _group = group;
        }

        public async Task<GroupMessage> CreateGroupMessage(Guid SenderId,CreateGroupMessageDto dto)
        {
            var message = _mapper.Map<GroupMessage>(dto);
            message.MessageId=Guid.NewGuid();
            // message.MessageId = Guid.NewGuid();

            var valid = await _group.UserPresentInGroup(SenderId,dto.GroupId);
            if (!valid)
            {
                return null;
            }

            // message.Group = await _group.GetGroupById(dto.GroupId);
            message.MessageSentBy=SenderId;            
            try
            {
                await _context.GroupMessages.AddAsync(message);
                await _context.SaveChangesAsync();
                return message;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteMessage(Guid MessageId)
        {
            var message = await _context.GroupMessages.FirstOrDefaultAsync(x=>x.MessageId==MessageId);
            if (message == null)
            {
                return false;
            }
            try
            {
                _context.GroupMessages.Remove(message);
                _context.Entry(message).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<GroupMessage>> GetAllGroupMessages(Guid groupId)
        {
            return await _context.GroupMessages.Where(x=>x.GroupId==groupId).ToListAsync();
        }

        public async Task<GroupMessage> GetMessageById(Guid MessageId)
        {
            return await _context.GroupMessages.FirstOrDefaultAsync(x=>x.MessageId==MessageId);
        }

        public async Task<GroupMessage> UpdateMessage(Guid MessageId, UpdateGroupMessageDto dto)
        {
            var message = await _context.GroupMessages.FirstOrDefaultAsync(x=>x.MessageId==MessageId);
            if (message == null)
            {
                return null;
            }
            try
            {
                message.MessageType = dto.MessageType;
                message.MessageBody = dto.MessageBody;
                message.EditedAt = DateTime.Now;
                _context.Entry(message).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return message;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<GroupMessage> DeliverMessageToUser(Guid MessageId, Guid UserId)
        {
            throw new NotImplementedException();
        }

        public Task<GroupMessage> UserSeenMessage(Guid MessageId, Guid UserId)
        {
            throw new NotImplementedException();
        }
    }
}
