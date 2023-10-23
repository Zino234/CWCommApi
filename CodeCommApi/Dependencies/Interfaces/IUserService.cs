using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCommApi.Dto;
using CodeCommApi.Models;

namespace CodeCommApi.Dependencies.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(CreateUserDto dto);
        Task<List<User>> GetUsers();
        Task<User> FindUser(Guid Id);
        Task<User> GetSingleUserById(Guid Id);
        Task<User> UpdateUser(Guid Id,UpdateUserDto dto);
        Task<List<Groups>> GetUserGroups(Guid Id);
        Task<bool> DeleteUser(Guid Id);
        bool FindAny(Guid Id);
    }
}