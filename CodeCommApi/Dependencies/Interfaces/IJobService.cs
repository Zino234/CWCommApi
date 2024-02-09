using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCommApi.Models;

namespace CodeCommApi.Dependencies.Interfaces
{
    public interface IJobService
    {
        Task<Job> CreateJob(Job job);
        Task<List<Job>> GetAllJobs();
        Task<List<Job>> GetAvailableJobs();
        Task<List<Job>> GetUserJobs(Guid UserId);
        Task<Job> EditJob(Guid JobId, Job job);
        Task<bool> DeleteJob(Guid JobId);
    }
}
