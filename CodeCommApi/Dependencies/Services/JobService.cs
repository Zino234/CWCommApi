using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommApi.Data;
using CodeCommApi.Dependencies.Interfaces;
using CodeCommApi.Models;

namespace CodeCommApi.Dependencies.Services
{
    public class JobService : IJobService
    {
        private readonly CodeCommDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _user;
        private readonly ILogger<GroupService> _logger;

        public Task<Job> CreateJob(Job job)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteJob(Guid JobId)
        {
            throw new NotImplementedException();
        }

        public Task<Job> EditJob(Guid JobId, Job job)
        {
            throw new NotImplementedException();
        }

        public Task<List<Job>> GetAllJobs()
        {
            throw new NotImplementedException();
        }

        public Task<List<Job>> GetAvailableJobs()
        {
            throw new NotImplementedException();
        }

        public Task<List<Job>> GetUserJobs(Guid UserId)
        {
            throw new NotImplementedException();
        }
    }
}