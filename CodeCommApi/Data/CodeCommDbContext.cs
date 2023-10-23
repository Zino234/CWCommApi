using CodeCommApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeCommApi.Data
{
    public class CodeCommDbContext : DbContext
    {
        public CodeCommDbContext(DbContextOptions<CodeCommDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Chat> Chats { get; set; }
    }
}
