using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCommApi.Dto.Groups.Response
{
    public class ReadGroupDto
    {
  
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string GroupLogo { get; set; }
        public DateTime GroupCreatedAt { get; set; }

        public Guid UserId { get; set; }
        public bool GroupIsDeleted { get; set; }
       
    }
}