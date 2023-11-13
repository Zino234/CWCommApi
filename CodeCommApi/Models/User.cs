using System.ComponentModel.DataAnnotations;

namespace CodeCommApi.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string? UserProfilePicUrl { get; set; }
        public string UserPassword { get; set; }
        public bool UserIsVerified { get; set; } = false;
        public bool UserAccountIsDisabled { get; set; } = false;
        public DateTime UserCreatedAt { get; set; }
    }
}
