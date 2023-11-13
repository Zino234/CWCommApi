using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommApi.Data;
using CodeCommApi.Dependencies.interfaces;
using CodeCommApi.Dependencies.Interfaces;
using CodeCommApi.Dto;
using CodeCommApi.Dto.Chats.Requests;
using CodeCommApi.Dto.Chats.Response;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CodeCommApi.Models.Services
{
    public class ChatService : IChatService
    {
        private readonly CodeCommDbContext _context;
        private readonly IMapper _mapper;
        private IUserService _user;

        public ChatService(CodeCommDbContext context, IMapper mapper, IUserService user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }

        public async Task<Chat> CreateChat(CreateChatDto dto)
        {
            Chat chat = _mapper.Map<Chat>(dto);
            chat.ChatId=Guid.NewGuid();
            var user1 = await _user.FindUser(dto.UserID1);
            var user2 = await _user.FindUser(dto.UserID2);
            if ((dto.UserID1 == dto.UserID2) || user1 == null || user2 == null)
            {
                return null;
            }
            // chat.User1 = user1;
            // chat.User2 = user2;

            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();
            return chat;
        }

        public async Task<bool> DeleteChat(Guid ChatId)
        {
            var result = await _context.Chats.FirstOrDefaultAsync(x => x.ChatId == ChatId);
            if (result == null)
            {
                return false;
            }
            _context.Chats.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Chat> GetChatById(Guid ChatId)
        {
            var result = await _context.Chats.FirstOrDefaultAsync(x =>x.ChatId==ChatId)!;
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<Chat> GetChatByUsers(Guid User1, Guid User2)
        {
         
            var result = await _context.Chats
                            .Where(
                                x =>
                                    (x.UserID1 == User1 && x.UserID2 == User2)
                                    || (x.UserID1 == User2 && x.UserID2 == User1)
                            )
                            .FirstOrDefaultAsync()!;
              if (User1 == User2||result==null)
            {
                return null;
            }

                
            return result;
        }
private async Task<User> TransformUser(Guid id){
    var user=await _user.FindUser(id);
    if(user ==null ){
        return null;
    }
    return user;
}
        public async Task<List<Chat>> GetUserChats(Guid UserId)
        {
            var result = await _context.Chats
                .Where(x => x.UserID1 == UserId || x.UserID2 == UserId)
                .ToListAsync()!;
            if (result == null)
            {
                return null;
            }
            // result.ForEach(async x =>{
            //     x.User1=await  TransformUser(x.UserID1);
            //     x.User2=await TransformUser(x.UserID2);
            // });

            return result;
        }
    }
}
