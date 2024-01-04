using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCommApi.Dto.Users.Requests
{
    public class UserLoginDto
    {
        [Required]
        public string? UsernameOrEmail { get; set; }

        // public string Username{ get; set; }

        [Required]
        public string? UserPassword { get; set; }
    }
}
