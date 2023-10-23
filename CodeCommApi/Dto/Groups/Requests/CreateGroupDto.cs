using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeCommApi.Models;

namespace CodeCommApi.Dto
{
    public class CreateGroupDto
    {
  
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string GroupLogo { get; set; }
        //  [ForeignKey("GroupCreatedBy")]
        public Guid UserId { get; set; }
        // public User GroupCreatedBy { get; set; }
    }
}
