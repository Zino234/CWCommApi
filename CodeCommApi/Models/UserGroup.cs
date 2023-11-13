using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeCommApi.Models
{
    [PrimaryKey("UserId","GroupId")]
    public class UserGroup
    {

        public Guid UserId { get; set; }

        public Guid GroupId { get; set; }

      
    }
}
