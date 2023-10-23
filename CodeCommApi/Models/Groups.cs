using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeCommApi.Models.Services;

namespace CodeCommApi.Models
{

    public class Groups
    {
        [Key]
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string GroupLogo { get; set; }
        public DateTime GroupCreatedAt { get; set; }

        [ForeignKey("GroupCreatedBy")]
        public Guid UserId { get; set; }
        // public static User GroupCreatedBy{get;set;}=await _repo.UpdateUserGroup(UserId);
        public User GroupCreatedBy { get; set; }
        public bool GroupIsDeleted { get; set; } = false;
        //public List<UserGroup>? Users  { get; set; }
    }
}
