using System.ComponentModel.DataAnnotations;

namespace CodeCommApi.Dto
{
    public class CreateUserDto
    {
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string? UserProfilePicUrl { get; set; }
        public string UserPassword { get; set; }

        [Compare("UserPassword")]
        public string ConfirmPassword { get; set; }
    }
}
