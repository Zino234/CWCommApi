using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeCommApi.Models
{
    [PrimaryKey("UserId","GroupId")]
    public class UserGroup
    {
        // [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }


        // [ForeignKey("Group")]
        public Guid GroupId { get; set; }
        public Groups Group { get; set; }
      
    }
}
