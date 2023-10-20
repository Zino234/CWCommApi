using System.ComponentModel.DataAnnotations;

namespace CodeCommApi.Dto
{
    public class CreateUserDto
    {
        [Key]
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? ProfilePicUrl { get; set; }
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
