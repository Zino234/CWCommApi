using CodeCommApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeCommApi.Data
{
    public class CodeCommDbContext : DbContext
    {
        public CodeCommDbContext(DbContextOptions<CodeCommDbContext> options)
            : base(options) { }

    //     protected override void OnModelCreating(ModelBuilder modelBuilder)
    //     {
    //         modelBuilder
    //             .Entity<Chat>()
    //             .HasOne(c => c.User1)
    //             .WithMany(u => u.User1Chats)
    //             .HasForeignKey(c => c.UserID1)
    //             .OnDelete(DeleteBehavior.Restrict);

    //         modelBuilder
    //             .Entity<Chat>()
    //             .HasOne(c => c.User2)
    //             .WithMany(u => u.User2Chats)
    //             .HasForeignKey(c => c.UserID2)
    //             .OnDelete(DeleteBehavior.Restrict);

    //         modelBuilder
    //             .Entity<Chat>()
    //             .HasOne(c => c.User1)
    //             .WithMany(u => u.User1Chats)
    //             .HasForeignKey(c => c.UserID1)
    //             .OnDelete(DeleteBehavior.Restrict);

    //         modelBuilder
    //             .Entity<Chat>()
    //             .HasOne(c => c.User2)
    //             .WithMany(u => u.User2Chats)
    //             .HasForeignKey(c => c.UserID2)
    //             .OnDelete(DeleteBehavior.Restrict);

    //         modelBuilder
    //             .Entity<DirectMessage>()
    //             .HasOne(d => d.MessageSender)
    //             .WithMany()
    //             .HasForeignKey(d => d.MessageSenderId)
    //             .OnDelete(DeleteBehavior.Restrict);

    //         modelBuilder
    //             .Entity<DirectMessage>()
    //             .HasOne(d => d.MessageReceiver)
    //             .WithMany()
    //             .HasForeignKey(d => d.MessageReceiverId)
    //             .OnDelete(DeleteBehavior.Restrict);



    //               modelBuilder.Entity<UserGroup>()
    //     .HasOne(ug => ug.User)
    //     .WithMany(u => u.UserGroups)
    //     .HasForeignKey(ug => ug.UserId)
    //     .OnDelete(DeleteBehavior.Restrict);

    // modelBuilder.Entity<UserGroup>()
    //     .HasOne(ug => ug.Group)
    //     .WithMany(g => g.UserGroups)
    //     .HasForeignKey(ug => ug.GroupId)
    //     .OnDelete(DeleteBehavior.Restrict);

    //         // Additional configurations for other entities if needed
        // }

        public DbSet<User> Users { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<DirectMessage> DirectMessages { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }
        public DbSet<GroupMessageStatus> GroupMessageStatuses { get; set; }
    }
}
