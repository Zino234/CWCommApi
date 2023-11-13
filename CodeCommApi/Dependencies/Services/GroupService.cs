using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommApi.Data;
using CodeCommApi.Dependencies.Interfaces;
using CodeCommApi.Dto;
using CodeCommApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeCommApi.Dependencies.Services
{
    public class GroupService : IGroupService
    {
        private readonly CodeCommDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _user;

        public GroupService(CodeCommDbContext context, IMapper mapper, IUserService user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }
        public async Task<bool> AddUserToGroup(Guid UserId, Guid GroupId)
        {
            User user = await _user.FindUser(UserId);
            Groups group = await GetGroupById(GroupId);
            if (user == null || group == null)
            {
                return false;
            }
            var UserGroup = new UserGroup()
            {
                UserId = UserId,
                GroupId = GroupId,
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
    var group=await GetGroupById(GroupId);
       
    var user=await _user.FindUser(UserId);
     if(group==null||user==null)
        {
            return false;
        }
    var userPresent=await _context.UserGroups.FirstOrDefaultAsync(x=>x.UserId==UserId&&x.GroupId==GroupId);
    if(userPresent==null){
        return false;
    }
    return true;
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

        public async Task<Groups> GetGroupById(Guid Id)
        {
            Groups group = await _context.Groups.FirstOrDefaultAsync(x => x.GroupId == Id);
            if (group == null)
            {
                return null;
            }
            return group;
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

    }
}