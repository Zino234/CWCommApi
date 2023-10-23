using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCommApi.Dto.Users.Response
{
    public class ReadUserDto
    {
              public Guid UserId { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string? UserProfilePicUrl { get; set; }
              public bool UserIsVerified { get; set; } 
        public bool UserAccountIsDisabled { get; set; }
        public DateTime UserCreatedAt { get; set; }
    }
}