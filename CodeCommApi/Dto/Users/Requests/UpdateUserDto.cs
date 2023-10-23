using System.ComponentModel.DataAnnotations;

namespace CodeCommApi.Dto
{
    public class UpdateUserDto
    {
        
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
        
        public string UserPhone { get; set; }
        public string? UserProfilePicUrl { get; set; }

        [Required]
        public string User2Password { get; set; }

        [Compare("UserPassword")]
        public string UserConfirmPassword { get; set; }

        public bool UserAccountIsDisabled { get; set; }
    }
}
