using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CodeCommApi.Models;

namespace CodeCommApi.Dto
{
    public class UpdateGroupDto
    {
          public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string GroupLogo { get; set; }
        //public DateTime GroupCreatedAt { get; set; }

       // [ForeignKey("GroupCreatedBy")]
       // public Guid UserId { get; set; }
       // public User GroupCreatedBy { get; set; }
        public bool GroupIsDeleted { get; set; }
        //public List<UserGroup> Users  { get; set; }
    }
}