using System.ComponentModel.DataAnnotations;

namespace CodeCommApi.Dto
{
    public class CreateGroupDto
    {
        [Key]
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string GroupLogo { get; set; }
    }
}
