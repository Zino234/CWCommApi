using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCommApi.Dto;
using CodeCommApi.Models;

namespace CodeCommApi.Dependencies.Interfaces
{
    public interface IGroupService
    {
        Task<Groups> CreateGroup(CreateGroupDto dto);
        Task<List<Groups>> GetAllGroups();
   Task<Groups> GetGroupById(Guid Id);
        Task<Groups> UpdateGroup(Guid Id,UpdateGroupDto dto);
        Task<bool> DeleteGroup(Guid Id);

        Task<bool> UserPresentInGroup(Guid UserId,Guid GroupId);


        Task<bool> AddUserToGroup(Guid UserId,Guid GroupId);

       
       
       
        Task<List<UserGroup>> LoadsUserGroups(string connectionId,Guid UserId);
        Task<bool> AddToGroup(string connectionId,Guid GroupId);
    }
}