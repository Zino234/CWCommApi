using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommApi.Data;
using CodeCommApi.Dependencies.Interfaces;
using CodeCommApi.Dto;
using BCrypt.Net;
using CodeCommApi.Dto.Users.Requests;
using CodeCommApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeCommApi.Dependencies.Services
{
    public class UserService : IUserService
    {
        private readonly CodeCommDbContext _context;
        private readonly IMapper _mapper;

        public UserService(CodeCommDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> CreateUser(CreateUserDto dto)
        {
            var check = await _context.Users.AnyAsync(
                x =>
                    x.Username == dto.Username
                    || x.UserEmail == dto.UserEmail
                    || x.UserPhone == dto.UserPhone
            );
            if (check)
            {
                return null;
            }
            User user = _mapper.Map<User>(dto);
            // user.UserId=Guid.NewGuid();
            user.UserPassword = BCrypt.Net.BCrypt.HashPassword(dto.UserPassword);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> FindUser(Guid Id)
        {
            var result = await _context.Users.Where(x => x.UserId == Id).FirstOrDefaultAsync();
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<bool> DeleteUser(Guid Id)
        {
            var User = await FindUser(Id);
            if (User == null)
            {
                return false;
            }
            _context.Users.Remove(User);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetSingleUserById(Guid Id)
        {
            return await FindUser(Id);
        }

        public async Task<List<Groups>> GetUserGroups(Guid Id)
        {
            var Users = await _context.Groups.Where(x => x.UserId == Id).ToListAsync();
            if (Users == null)
            {
                return null;
            }
            return Users;
        }

        public async Task<List<User>> GetUsers()
        {
            var result = await _context.Users.ToListAsync();
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public bool FindAny(Guid Id)
        {
            return _context.Users.Any(x => x.UserId == Id);
        }

        public async Task<User> UpdateUser(Guid Id, UpdateUserDto dto)
        {
            var result = await FindUser(Id);
            if (result == null)
            {
                return null;
            }
            result.Username = dto.Username;
            result.UserPhone = dto.UserPhone;
            result.UserEmail = dto.UserEmail;
            result.UserProfilePicUrl = dto.UserProfilePicUrl;
            result.UserPassword = dto.UserConfirmPassword;
            result.UserAccountIsDisabled = dto.UserAccountIsDisabled;
            result.UserIsVerified = dto.UserIsVerified;
            _context.Entry(result).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<User> UserLogin(UserLoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(
                x => (x.UserEmail == dto.UsernameOrEmail || x.Username == dto.UsernameOrEmail)
            );

            if (BCrypt.Net.BCrypt.Verify(dto.UserPassword, user.UserPassword))
            {
                return user;
            }
            return null;
        }
    }
}
