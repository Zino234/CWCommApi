using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommApi.Data;
using CodeCommApi.Dependencies.interfaces;
using CodeCommApi.Dto;
using CodeCommApi.Dto.Chats.Requests;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CodeCommApi.Models.Services
{
    public class ChatService : IChatService
    {
              private readonly CodeCommDbContext _context;
              private readonly IMapper _mapper;
        public ChatService(CodeCommDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
            
        }
        public async Task<Chat> CreateChat(CreateChatDto dto)
        {
            
            Chat chat=_mapper.Map<Chat>(dto);
    
           await _context.Chats.AddAsync(chat);
           await _context.SaveChangesAsync();
           return chat;
        }

        public async Task<bool> DeleteChat(Guid ChatId)
        {
           var result=await _context.Chats.FindAsync(ChatId);
           if(result==null){
            return false;
           }
           _context.Chats.Remove(result);
           await _context.SaveChangesAsync();
           return true;
        }

        public async Task<Chat> GetChatById(Guid ChatId)
        {
            var  result=await _context.Chats.FindAsync(ChatId)!;
            if(result==null){
                return null;
            }
            return result;
        }

        public async  Task<Chat> GetChatByUsers(Guid User1, Guid User2)
        {
            var result=await _context.Chats.Where(x=>(x.UserID1==User1&&x.UserID2==User2)||(x.UserID1==User2&&x.UserID2==User1)).FirstOrDefaultAsync()!;
            return result;
        }

        public async Task<List<Chat>> GetUserChats(Guid UserId)
        {
            var result=await _context.Chats.Where(x=>x.UserID1==UserId||x.UserID2 ==UserId).ToListAsync()!;
            return result;
        }
    }
}