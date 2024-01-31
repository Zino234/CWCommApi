using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommApi.Data;
using CodeCommApi.Dependencies.Interfaces;
using CodeCommApi.Dto;
using CodeCommApi.Models;
using CodeCommApi.Models.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CodeCommApi.Dependencies.Services
{
    public class GroupService : IGroupService
    {
        private readonly CodeCommDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _user;
        private readonly ILogger<GroupService> _logger;

        // private readonly CodeCommGroupHub _cg;
        private readonly IHubContext<CodeCommGroupHub> _groupHub;
        private readonly HttpContext _ctx;

        public GroupService(CodeCommDbContext context,IHubContext<CodeCommGroupHub> groupHub, IMapper mapper, IUserService user,ILogger<GroupService> logger)
        {
            _context = context;
            _groupHub = groupHub;
            _mapper = mapper;
            _user = user;
            _logger = logger;
        }

        
        public async Task<bool> AddUserToGroup(Guid UserId, Guid GroupId)
        {
            User user = await _user.FindUser(UserId);
            Groups group = await GetGroupById(GroupId);
            if (user == null || group == null)
            {
                var errorEntity=(user==null)?"USER":"GROUP";
                throw new NullReferenceException($"AN ERORR OCCURED | INVALID {errorEntity} ID");
            }
            var UserGroup = new UserGroup()
            {
                UserId = UserId,
                GroupId = GroupId,
                //THIS PART IS FOR IF YOU WANT TO GET THE GROUPS AS WELL .
                // User = user,
                // Group = group
            };
            try
            {
                await _context.UserGroups.AddAsync(UserGroup);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {

                throw;
            }

        }




        public async Task<Groups> CreateGroup(CreateGroupDto dto)
        {
            User user = await _user.FindUser(dto.UserId);
            var group = _mapper.Map<Groups>(dto);
            group.GroupCreatedBy = user;
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return group;
        }


public async Task<bool> UserPresentInGroup(Guid UserId,Guid GroupId){
    return await _context.UserGroups.AnyAsync(x=>x.UserId==UserId&&x.GroupId==GroupId);
}



        public async Task<bool> DeleteGroup(Guid Id)
        {
            Groups group = await GetGroupById(Id);
            if (group != null)
            {
                _context.Groups.Remove(group);
                _context.Entry(group).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Groups>> GetAllGroups()
        {
            var result = await _context.Groups.Where(x => x.GroupIsDeleted == false).ToListAsync();
            if (result != null)
            {
                return result;
            };
            return null;
        }







        public async Task<Groups> UpdateGroup(Guid Id, UpdateGroupDto dto)
        {
            Groups result = await GetGroupById(Id);
            if (result != null)
            {
                result.GroupName = dto.GroupName;
                result.GroupDescription = dto.GroupDescription;
                result.GroupLogo = dto.GroupLogo;
                result.GroupIsDeleted = dto.GroupIsDeleted;
                // result.Users = dto.Users;
                _context.Entry(result).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }



        public async Task<Groups> GetGroupById(Guid Id)
        {
            Groups group = await _context.Groups.FirstOrDefaultAsync(x => x.GroupId == Id);
            if (group == null)
            {
                return null;
            }
            return group;
        }





















































        public async Task<List<UserGroup>> LoadsUserGroups(string connectionId,Guid UserId)
        {


            var result = await _context.UserGroups.Where(x => x.UserId == UserId).Include(x => x.Group).ToListAsync();
            if (result != null)
            {
                result.Select(async x =>
                {
                    await AddToGroup(connectionId,x.GroupId);
                });
                return result;

            };
            return null;
        }




        public async Task<bool> AddToGroup(string connectionId,Guid GroupId){
           await  _groupHub.Groups.AddToGroupAsync(connectionId,GroupId.ToString());
           return true;
        }



















    }
}